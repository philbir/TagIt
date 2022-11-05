using MongoDB.Driver;
using MongoDB.Extensions.Context;

namespace TagIt.Store.Mongo;

internal class ThingClassCollectionConfiguration :
    IMongoCollectionConfiguration<ThingClass>
{
    public void OnConfiguring(
        IMongoCollectionBuilder<ThingClass> builder)
    {
        builder.WithDefaults(CollectionNames.ThingClass)
            .WithCollectionConfiguration(collection =>
            {
                var nameIndex = new CreateIndexModel<ThingClass>(
                     Builders<ThingClass>.IndexKeys
                         .Ascending(c => c.Name),
                     new CreateIndexOptions { Unique = true });

                collection.Indexes.CreateOne(nameIndex);
            });
    }
}
