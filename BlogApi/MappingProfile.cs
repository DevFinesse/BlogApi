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
        }
    }
}
