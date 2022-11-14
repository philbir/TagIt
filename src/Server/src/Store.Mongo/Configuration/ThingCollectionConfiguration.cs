using MongoDB.Extensions.Context;

namespace TagIt.Store.Mongo;

internal class ThingCollectionConfiguration :
    IMongoCollectionConfiguration<Thing>
{
    public void OnConfiguring(
        IMongoCollectionBuilder<Thing> builder)
    {
        builder.WithDefaults(CollectionNames.Thing)
            .AddBsonClassMap<ImageData>(cm =>
            {
                cm.UnmapField(x => x.Data);
            })
            .WithCollectionConfiguration(collection =>
            {

            });
    }
}
