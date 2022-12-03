using MongoDB.Driver;
using MongoDB.Extensions.Context;

namespace TagIt.Store.Mongo;

internal class TagDefintionCollectionConfiguration :
    IMongoCollectionConfiguration<TagDefintion>
{
    public void OnConfiguring(
        IMongoCollectionBuilder<TagDefintion> builder)
    {
        builder.WithDefaults(CollectionNames.TagDefintion)
            .WithCollectionConfiguration(collection =>
            {
                var nameIndex = new CreateIndexModel<TagDefintion>(
                     Builders<TagDefintion>.IndexKeys
                         .Ascending(c => c.Name),
                     new CreateIndexOptions { Unique = true });

                collection.Indexes.CreateOne(nameIndex);
            });
    }
}
