using MongoDB.Driver;
using MongoDB.Extensions.Context;

namespace TagIt.Store.Mongo;

public static class MongoCollectionConfigurationExtensions
{
    public static IMongoCollectionBuilder<TDocument> WithDefaults<TDocument>(
        this IMongoCollectionBuilder<TDocument> builder,
        string collectionName,
        bool autoMap = true)
            where TDocument : class, IEntity, new()
    {
        builder
            .WithCollectionName(collectionName)
            .WithCollectionSettings(s => s.ReadConcern = ReadConcern.Majority)
            .WithCollectionSettings(s => s.ReadPreference = ReadPreference.Nearest);

        if (autoMap)
        {
            builder.AddBsonClassMap<Receiver>(cm =>
            {
                cm.AutoMap();
            });
        }

        return builder;
    }
}
