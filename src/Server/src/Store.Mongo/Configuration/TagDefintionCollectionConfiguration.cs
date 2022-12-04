using MongoDB.Driver;
using MongoDB.Extensions.Context;

namespace TagIt.Store.Mongo;

internal class TagDefintionCollectionConfiguration :
    IMongoCollectionConfiguration<TagDefinition>
{
    public void OnConfiguring(
        IMongoCollectionBuilder<TagDefinition> builder)
    {
        builder.WithDefaults(CollectionNames.TagDefintion)
            .WithCollectionConfiguration(collection =>
            {
                var nameIndex = new CreateIndexModel<TagDefinition>(
                     Builders<TagDefinition>.IndexKeys
                         .Ascending(c => c.Name),
                     new CreateIndexOptions { Unique = true });

                collection.Indexes.CreateOne(nameIndex);
            });
    }
}
