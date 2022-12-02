using TagIt.Store;

namespace TagIt.GraphQL;

public partial class ThingGraphQLType : ObjectType<Thing>
{
    protected override void Configure(IObjectTypeDescriptor<Thing> descriptor)
    {
        descriptor.Field(x => x.Id).ID();
        descriptor.Field(x => x.CorespondentId).ID(nameof(Correspondent));
        descriptor.Field(x => x.ReceiverId).ID(nameof(Receiver));
        descriptor.Field(x => x.ClassId).Ignore();
        descriptor.Field(x => x.TypeId).Ignore();

        descriptor.Ignore(x => x.Thumbnails);
        descriptor.Ignore(x => x.Data);

        descriptor.Field("type")
            .ResolveWith<Resolvers>(x => x.GetTypeAsync(default!, default!, default!));

        descriptor.Field("class")
            .ResolveWith<Resolvers>(x => x.GetClassAsync(default!, default!, default!));

        descriptor.Field("thumbnail")
            .Argument("pageNumber", a => a
                .DefaultValue(1)
                .Type(typeof(int)))
            .Argument("loadData", a => a
                .DefaultValue(false)
                .Type(typeof(bool)))
            .ResolveWith<Resolvers>(x => x
                .GetThumbnailAsync(default!, default!, default!, default!, default!));
    }

    class Resolvers
    {
        internal async Task<ThingThumbnail?> GetThumbnailAsync(
            [Service] IThumbnailStore store,
            [Parent] Thing thing,
            int pageNumber,
            bool loadData,
            CancellationToken cancellationToken)
        {
            ThingThumbnail? thumbnail = thing.Thumbnails.FirstOrDefault();

            if (thumbnail is not null)
            {
                thumbnail.Data = await store.GetAsync(thumbnail.FileId, cancellationToken);
            }

            return thumbnail;
        }

        internal async Task<ThingType?> GetTypeAsync(
            [Parent] Thing thing,
            [DataLoader] ThingTypeByIdDataLoader dataLoader,
            CancellationToken cancellationToken)
        {
            if (thing.TypeId is { })
            {
                return await dataLoader.LoadAsync(thing.TypeId.Value, cancellationToken);
            }

            return null;
        }

        internal async Task<ThingClass?> GetClassAsync(
            [Parent] Thing thing,
            [DataLoader] ThingClassByIdDataLoader dataLoader,
            CancellationToken cancellationToken)
        {
            if (thing.ClassId is { })
            {
                return await dataLoader.LoadAsync(thing.ClassId.Value, cancellationToken);
            }

            return null;
        }
    }
}


