using MongoDB.Driver;
using MongoDB.Extensions.Context;

namespace TagIt.Store.Mongo;

internal class PropertyDefinitionCollectionConfiguration :
    IMongoCollectionConfiguration<PropertyDefinition>
{
    public void OnConfiguring(
        IMongoCollectionBuilder<PropertyDefinition> builder)
    {
        builder
            .WithDefaults(CollectionNames.ConnectorDefintion)
            .WithCollectionConfiguration(collection =>
            {
                var nameIndex = new CreateIndexModel<PropertyDefinition>(
                     Builders<PropertyDefinition>.IndexKeys
                         .Ascending(c => c.Name),
                     new CreateIndexOptions { Unique = true });

                collection.Indexes.CreateOne(nameIndex);
            });
    }
}
