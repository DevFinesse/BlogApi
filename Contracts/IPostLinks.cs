using Entities.LinkModels;
using Microsoft.AspNetCore.Http;
using Shared.DataTransferObjects;

namespace Contracts
{
    public interface IPostLinks
    {
        LinkResponse TryGenerateLinks(IEnumerable<PostDto> postsDto, string fields, HttpContext httpContext);
    }
}
