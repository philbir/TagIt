
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace TagIt.Store.Mongo;

public class WorkflowStore : Store<Workflow>, IWorkflowStore
{
    public WorkflowStore(ITagIdDbContext dbContext)
        : base(dbContext)
    {
    }

    IQueryable<Workflow> IWorkflowStore.Query()
    {
        return Query;
    }

    public async Task UpdateStepAsync(Guid workflowId,  WorkflowStep step, CancellationToken cancellationToken)
    {
        FilterDefinition<Workflow> filter = Builders<Workflow>.Filter.Eq(x => x.Id, workflowId)
                                             & Builders<Workflow>.Filter.ElemMatch(
                                                 x => x.Steps,
                                                 Builders<WorkflowStep>.Filter.Eq(x => x.Id, step.Id));

        UpdateDefinition<Workflow> update = Builders<Workflow>.Update.Set(x => x.Steps[-1], step);

        UpdateResult updateResult = await Collection.UpdateOneAsync(filter, update, new UpdateOptions(), cancellationToken);
    }
}
