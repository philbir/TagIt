using MongoDB.Driver;
using MongoDB.Extensions.Context;

namespace TagIt.Store.Mongo;

internal class ConnectorDefintionCollectionConfiguration :
    IMongoCollectionConfiguration<ConnectorDefintion>
{
    public void OnConfiguring(
        IMongoCollectionBuilder<ConnectorDefintion> builder)
    {
        builder
            .WithDefaults(CollectionNames.ConnectorDefintion)
            .WithCollectionConfiguration(collection =>
            {
                var nameIndex = new CreateIndexModel<ConnectorDefintion>(
                     Builders<ConnectorDefintion>.IndexKeys
                         .Ascending(c => c.Name),
                     new CreateIndexOptions { Unique = true });

                collection.Indexes.CreateOne(nameIndex);
            });
    }
}
