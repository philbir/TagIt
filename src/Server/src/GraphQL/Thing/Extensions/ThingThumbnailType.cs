using System.Xml.Linq;
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

            return $"/api/thing/thumbnail/{thumbnail.FileId}/{thumbnail.Format}";
        }
    }
}
