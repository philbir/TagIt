using MongoDB.Driver;
using MongoDB.Extensions.Context;

namespace TagIt.Store.Mongo;

internal class ThingTypeCollectionConfiguration :
    IMongoCollectionConfiguration<ThingType>
{
    public void OnConfiguring(
        IMongoCollectionBuilder<ThingType> builder)
    {
        builder.WithDefaults(CollectionNames.ThingType)
            .WithCollectionConfiguration(collection =>
            {
                var nameIndex = new CreateIndexModel<ThingType>(
                     Builders<ThingType>.IndexKeys
                         .Ascending(c => c.Name),
                     new CreateIndexOptions { Unique = true });

                collection.Indexes.CreateOne(nameIndex);

                var typeMapIndex = new CreateIndexModel<ThingType>(
                     Builders<ThingType>.IndexKeys
                         .Ascending(c => c.TypeMap),
                     new CreateIndexOptions { Unique = false });

                collection.Indexes.CreateManyAsync(
                    new List<CreateIndexModel<ThingType>>() { nameIndex, typeMapIndex});

            });
    }
}
