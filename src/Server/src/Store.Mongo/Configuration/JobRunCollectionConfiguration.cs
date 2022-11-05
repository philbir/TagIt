using MongoDB.Driver;
using MongoDB.Extensions.Context;

namespace TagIt.Store.Mongo;

internal class JobRunCollectionConfiguration :
    IMongoCollectionConfiguration<JobRun>
{
    public void OnConfiguring(
        IMongoCollectionBuilder<JobRun> builder)
    {
        builder
            .WithDefaults(CollectionNames.JobRun, autoMap: false)
            .AddBsonClassMap<JobRun>(cm =>
            {
                cm.AutoMap();
                cm.UnmapMember(c => c.JobDefintion);
            })
            .WithCollectionSettings(s => s.ReadConcern = ReadConcern.Majority)
            .WithCollectionSettings(s => s.ReadPreference = ReadPreference.Nearest);
    }
}
