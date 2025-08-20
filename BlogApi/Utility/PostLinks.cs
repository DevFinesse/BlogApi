using Contracts;
using Entities.LinkModels;
using Entities.Models;
using Microsoft.Net.Http.Headers;
using Shared.DataTransferObjects;

namespace BlogApi.Utility
{
    public class PostLinks : IPostLinks
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly IDataShaper<PostDto> _dataShaper;

        public PostLinks(LinkGenerator linkGenerator, IDataShaper<PostDto> dataShaper)
        {
            _linkGenerator = linkGenerator;
            _dataShaper = dataShaper;
        }

        public LinkResponse TryGenerateLinks(IEnumerable<PostDto> postsDto, string fields, HttpContext httpContext)
        {
            var shapedPosts = ShapeData(postsDto, fields);

            if (ShouldGenerateLinks(httpContext))
                return ReturnLinkedPosts(postsDto, fields, httpContext, shapedPosts);

            return ReturnShapedPosts(shapedPosts);
        }

        private List<Entity> ShapeData(IEnumerable<PostDto> postDtos, string fields) 
        { 
            return _dataShaper.ShapeData(postDtos, fields)
                .Select(p => p.Entity)
                .ToList();
        }

        private bool ShouldGenerateLinks(HttpContext httpContext) 
        {
            var mediaType = (MediaTypeHeaderValue)httpContext.Items["AcceptHeaderMediaType"];
            return mediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
        }

        private LinkResponse ReturnShapedPosts(List<Entity> shapedPosts) =>
            new LinkResponse { ShapedEntities = shapedPosts };

        private LinkResponse ReturnLinkedPosts(IEnumerable<PostDto> postDto, string fields, HttpContext httpContext, List<Entity> shapedPosts) 
        { 
            var postDtoList = postDto.ToList();

            for (var index = 0; index < postDtoList.Count; index++)
            {
                var postLinks = CreateLinksForPost(httpContext, postDtoList[index].Id, fields);
                shapedPosts[index].Add("Links", postLinks);
            }

            var postCollection = new LinkCollectionWrapper<Entity>(shapedPosts);
            var linkedPosts = CreateLinksForPosts(httpContext, postCollection);

            return new LinkResponse {HasLink = true, LinkedEntities = linkedPosts};
        }

        private List<Link> CreateLinksForPost(HttpContext httpContext,Guid id, string fields = "")
        {
            var links = new List<Link>
            {
                new Link(_linkGenerator.GetUriByAction(httpContext, "GetPost", values: new {id, fields}), "self", "GET"),
                new Link(_linkGenerator.GetUriByAction(httpContext, "DeletePost", values: new {id}), "delete_post", "DELETE"),
                new Link(_linkGenerator.GetUriByAction(httpContext, "UpdatePost", values: new {id}), "update_post", "PUT"),
                new Link(_linkGenerator.GetUriByAction(httpContext, "PartiallyUpdatedPost", values: new {id}), "partially_update_post", "PATCH")
            };

            return links;
        }

        private LinkCollectionWrapper<Entity> CreateLinksForPosts(HttpContext httpContext, LinkCollectionWrapper<Entity> postWrapper)
        {
            postWrapper.Links.Add(new Link(_linkGenerator.GetUriByAction(httpContext, "GetPosts", values: new { }), "self", "GET"));

            return postWrapper;
        }
    }
}
