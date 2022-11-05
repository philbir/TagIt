using MongoDB.Driver;
using MongoDB.Extensions.Context;

namespace TagIt.Store.Mongo;

internal class UserCollectionConfiguration :
    IMongoCollectionConfiguration<User>
{
    public void OnConfiguring(
        IMongoCollectionBuilder<User> builder)
    {
        builder.WithDefaults(CollectionNames.User, autoMap: false)
            .AddBsonClassMap<User>(cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(c => c.Id);
            })
            .WithCollectionConfiguration(collection =>
            {
                var emailIndex = new CreateIndexModel<User>(
                     Builders<User>.IndexKeys
                         .Ascending(c => c.Email),
                     new CreateIndexOptions { Unique = true });

                collection.Indexes.CreateOne(emailIndex);
            });
    }
}
