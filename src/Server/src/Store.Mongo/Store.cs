using MongoDB.Driver;

namespace TagIt.Store.Mongo;

public class Store<TEntity> : IStore<TEntity> where TEntity : class, IEntity, new()
{
    protected readonly ITagIdDbContext _dbContext;

    public Store(ITagIdDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IMongoCollection<TEntity> Collection
        => _dbContext.GetCollection<TEntity>();

    public IMongoQueryable<TEntity> Query
        => Collection.AsQueryable();

    public virtual async Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        TEntity entity = await Query
            .Where(x => x.Id == id)
            .SingleOrDefaultAsync(cancellationToken);

        return entity;
    }

    public virtual async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await Query
            .ToListAsync(cancellationToken);
    }

    public virtual async Task<IReadOnlyList<TEntity>> GetManyAsync(
        IEnumerable<Guid> ids,
        CancellationToken cancellationToken)
    {
        return await Query
            .Where(x => ids.Contains(x.Id))
            .ToListAsync(cancellationToken);
    }

    public virtual async Task<TEntity> InsertAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await Collection.InsertOneAsync(
            entity,
            new InsertOneOptions(),
            cancellationToken);

        return entity;
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await Collection.ReplaceOneAsync(
            x => x.Id == entity.Id,
            entity,
            new ReplaceOptions(),
            cancellationToken);

        return entity;
    }

    public virtual async Task<long> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        DeleteResult result = await Collection.DeleteOneAsync(
            x => x.Id ==  id,
            new DeleteOptions(),
            cancellationToken);

        return result.DeletedCount;
    }

    public virtual async Task<TEntity> SaveAsync(
        TEntity entity,
        CancellationToken cancellationToken)
    {
        FilterDefinition<TEntity>? filter = Builders<TEntity>.Filter.Eq(x => x.Id, entity.Id);
        ReplaceOptions options = new() { IsUpsert = true };

        await Collection.ReplaceOneAsync(filter, entity, options, cancellationToken);

        return entity;
    }
}
