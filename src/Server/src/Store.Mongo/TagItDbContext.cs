using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using MongoDB.Extensions.Context;

namespace TagIt.Store.Mongo;
internal class TagIdDbContext : MongoDbContext, ITagIdDbContext
{
    public TagIdDbContext(IOptionsMonitor<MongoOptions> mongoOptions)
        : base(mongoOptions.Get(nameof(TagIdDbContext)))
    {
    }

    protected override void OnConfiguring(IMongoDatabaseBuilder builder)
    {
        builder
            .RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String))
                .RegisterConventionPack(
                    "Conventions",
                    new ConventionPack {
                        new EnumRepresentationConvention(BsonType.String),
                        new IgnoreExtraElementsConvention(true)
                    }, filter => true)
            .ConfigureConnection(con => con.ReadConcern = ReadConcern.Majority)
            .ConfigureConnection(con => con.WriteConcern = WriteConcern.WMajority)
            .ConfigureConnection(con => con.ReadPreference = ReadPreference.Primary)
            .ConfigureCollection(new ThingCollectionConfiguration())
            .ConfigureCollection(new ThingTypeCollectionConfiguration())
            .ConfigureCollection(new ThingClassCollectionConfiguration())
            .ConfigureCollection(new UserCollectionConfiguration())
            .ConfigureCollection(new ConnectorDefintionCollectionConfiguration())
            .ConfigureCollection(new CredentialCollectionConfiguration())
            .ConfigureCollection(new ReceiverCollectionConfiguration())
            .ConfigureCollection(new CorrespondentCollectionConfiguration())
            .ConfigureCollection(new JobDefintionCollectionConfiguration())
            .ConfigureCollection(new AuthStateCollectionConfiguration())
            .ConfigureCollection(new JobRunCollectionConfiguration())
            .ConfigureCollection(new WebHookCollectionConfiguration())
            .ConfigureCollection(new EntityAuditEventConfiguration());
    }

    public IGridFSBucket CreateGridFsBucket(string name)
    {
        return new GridFSBucket(Database, new GridFSBucketOptions
        {
            BucketName = name,
        });
    }

    public IMongoCollection<TEntity> GetCollection<TEntity>() where TEntity : class, IEntity
        => CreateCollection<TEntity>();

    public IMongoQueryable<TEntity> Query<TEntity>() where TEntity : class, IEntity
        => CreateCollection<TEntity>().AsQueryable();

    public IMongoCollection<User> Users => CreateCollection<User>();

    public IMongoCollection<Thing> Things => CreateCollection<Thing>();
}
