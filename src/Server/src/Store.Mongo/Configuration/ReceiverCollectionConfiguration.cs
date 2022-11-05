using MongoDB.Driver;
using MongoDB.Extensions.Context;

namespace TagIt.Store.Mongo;

internal class ReceiverCollectionConfiguration :
    IMongoCollectionConfiguration<Receiver>
{
    public void OnConfiguring(
        IMongoCollectionBuilder<Receiver> builder)
    {
        builder.WithDefaults(CollectionNames.Receiver)
            .WithCollectionConfiguration(collection =>
            {
                var nameIndex = new CreateIndexModel<Receiver>(
                     Builders<Receiver>.IndexKeys
                         .Ascending(c => c.Name),
                     new CreateIndexOptions { Unique = true });

                collection.Indexes.CreateOne(nameIndex);
            });
    }
}
