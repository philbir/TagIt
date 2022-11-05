using MongoDB.Driver;
using MongoDB.Extensions.Context;

namespace TagIt.Store.Mongo;

internal class JobDefintionCollectionConfiguration :
    IMongoCollectionConfiguration<JobDefintion>
{
    public void OnConfiguring(
        IMongoCollectionBuilder<JobDefintion> builder)
    {
        builder.WithDefaults(CollectionNames.JobDefintion)
            .WithCollectionConfiguration(collection =>
            {
                var nameIndex = new CreateIndexModel<JobDefintion>(
                     Builders<JobDefintion>.IndexKeys
                         .Ascending(c => c.Name),
                     new CreateIndexOptions { Unique = true });

                collection.Indexes.CreateOne(nameIndex);
            });
    }
}

internal class AuthStateCollectionConfiguration :
    IMongoCollectionConfiguration<ClientAuthState>
{
    public void OnConfiguring(
        IMongoCollectionBuilder<ClientAuthState> builder)
    {
        builder.WithDefaults(CollectionNames.AuthState)
            .WithCollectionConfiguration(collection =>
            {
                var createdTtlIndex = new CreateIndexModel<ClientAuthState>(
                     Builders<ClientAuthState>.IndexKeys
                         .Ascending(c => c.CreatedAt),
                     new CreateIndexOptions { ExpireAfter = TimeSpan.FromMinutes(2) });

                collection.Indexes.CreateOne(createdTtlIndex);
            });
    }
}

