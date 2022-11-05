using MongoDB.Driver;
using MongoDB.Extensions.Context;

namespace TagIt.Store.Mongo;

internal class CorrespondentCollectionConfiguration :
    IMongoCollectionConfiguration<Correspondent>
{
    public void OnConfiguring(
        IMongoCollectionBuilder<Correspondent> builder)
    {
        builder
            .WithDefaults(CollectionNames.Correspondent)
            .WithCollectionConfiguration(collection =>
            {
                var nameIndex = new CreateIndexModel<Correspondent>(
                     Builders<Correspondent>.IndexKeys
                         .Ascending(c => c.Name),
                     new CreateIndexOptions { Unique = true });

                collection.Indexes.CreateOne(nameIndex);
            });
    }
}

