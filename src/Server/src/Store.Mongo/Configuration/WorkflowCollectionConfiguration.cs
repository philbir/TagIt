using MongoDB.Extensions.Context;

namespace TagIt.Store.Mongo;

internal class WorkflowCollectionConfiguration :
    IMongoCollectionConfiguration<Workflow>
{
    public void OnConfiguring(
        IMongoCollectionBuilder<Workflow> builder)
    {
        builder
            .WithDefaults(CollectionNames.Workflow);
    }
}
