using MongoDB.Extensions.Context;

namespace TagIt.Store.Mongo;

internal class ThingContentConfiguration :
    IMongoCollectionConfiguration<ThingContent>
{
    public void OnConfiguring(
        IMongoCollectionBuilder<ThingContent> builder)
    {
        builder.WithDefaults(CollectionNames.ThingContent, autoMap: false)
            .AddBsonClassMap<ThingContent>(cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(c => c.Id);
            })
            .AddBsonClassMap<PageTextContent>(cm =>
            {
                cm.AutoMap();
            });
    }
}
