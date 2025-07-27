using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace BlogApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        { 
            CreateMap<Category, CategoryDto>();
            CreateMap<Comment, CommentDto>();
            CreateMap<Post, PostDto>();
            CreateMap<PostCreationDto, Post>().ForMember(dest => dest.Slug, opt => opt.Ignore());
        }
    }
}
