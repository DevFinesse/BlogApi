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
            CreateMap<CategoryCreationDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();
            CreateMap<Comment, CommentDto>();
            CreateMap<Post, PostDto>();
            CreateMap<PostCreationDto, Post>().ForMember(dest => dest.Slug, opt => opt.Ignore());
            CreateMap<CommentCreationDto, Comment>();
            CreateMap<CommentUpdateDto, Comment>();
            CreateMap<PostUpdateDto, Post>().ReverseMap();
        
        }
    }
}
