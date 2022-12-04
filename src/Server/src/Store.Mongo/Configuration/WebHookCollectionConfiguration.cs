using MongoDB.Extensions.Context;

namespace TagIt.Store.Mongo;

internal class WebHookCollectionConfiguration :
    IMongoCollectionConfiguration<WebHook>
{
    public void OnConfiguring(
        IMongoCollectionBuilder<WebHook> builder)
    {
        builder
            .WithDefaults(CollectionNames.WebHook);
    }
}
