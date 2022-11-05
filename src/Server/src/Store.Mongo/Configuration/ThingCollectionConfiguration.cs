using MongoDB.Extensions.Context;

namespace TagIt.Store.Mongo;

internal class ThingCollectionConfiguration :
    IMongoCollectionConfiguration<Thing>
{
    public void OnConfiguring(
        IMongoCollectionBuilder<Thing> builder)
    {
        builder.WithDefaults(CollectionNames.Thing)
            .WithCollectionConfiguration(collection =>
            {
            });
    }
}
