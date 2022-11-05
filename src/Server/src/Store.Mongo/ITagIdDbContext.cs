using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace TagIt.Store.Mongo;

public interface ITagIdDbContext
{
    //IMongoCollection<User> Users { get; }
    //IMongoCollection<Thing> Things { get; }

    IGridFSBucket CreateGridFsBucket(string name);
    IMongoCollection<TEntity> GetCollection<TEntity>() where TEntity : class, IEntity;
    IMongoQueryable<TEntity> Query<TEntity>() where TEntity : class, IEntity;
}
