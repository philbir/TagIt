using MongoDB.Driver;
using MongoDB.Extensions.Context;

namespace TagIt.Store.Mongo;

internal class CredentialCollectionConfiguration :
    IMongoCollectionConfiguration<Credential>
{
    public void OnConfiguring(
        IMongoCollectionBuilder<Credential> builder)
    {
        builder.WithDefaults(CollectionNames.Credentials)
            .AddBsonClassMap<ProtectedValue>(cm =>
            {
                cm.AutoMap();
                cm.UnmapMember(c => c.Value);
            })
            .WithCollectionConfiguration(collection =>
            {
                var nameIndex = new CreateIndexModel<Credential>(
                     Builders<Credential>.IndexKeys
                         .Ascending(c => c.Name),
                     new CreateIndexOptions { Unique = true });

                collection.Indexes.CreateOne(nameIndex);
            });
    }
}
