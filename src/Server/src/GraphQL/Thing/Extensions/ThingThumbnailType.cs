using TagIt.Image;
using TagIt.Store;

namespace TagIt.GraphQL;

public partial class ThingThumbnailType : ObjectType<ThingThumbnail>
{
    protected override void Configure(IObjectTypeDescriptor<ThingThumbnail> descriptor)
    {
        descriptor.Ignore(x => x.Data);
        descriptor.Ignore(x => x.FileId);

        descriptor
            .Field("url")
            .Type(typeof(string))
            .ResolveWith<Resolvers>(x => x.GetUrl(default!));
    }

    class Resolvers
    {
        internal string GetUrl([Parent] ThingThumbnail thumbnail)
        {
            if (thumbnail.Data is not null)
            {
                return thumbnail.Data.ToImageDataUrl(thumbnail.Format);
            }

            return $"/api/thing/thumbnail/{thumbnail.FileId}";
        }
    }
}

public partial class ThingGraphQLType : ObjectType<Thing>
{
    protected override void Configure(IObjectTypeDescriptor<Thing> descriptor)
    {
        descriptor.Ignore(x => x.Thumbnails);
        descriptor.Ignore(x => x.Data);

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
    }
}


