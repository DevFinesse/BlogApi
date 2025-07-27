using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service
{
    internal sealed class CategoryService : ICategoryService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CategoryService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public IEnumerable<CategoryDto> GetAllCategories(bool trackChanges)
        {
                var categories = _repository.CategoryRepository.GetAllCategories(trackChanges);
                var categoriesDto = _mapper.Map<IEnumerable<CategoryDto>>(categories);
                return categoriesDto;
         }

        public CategoryDto GetCategory(Guid id, bool trackChanges)
        { 
            var category = _repository.CategoryRepository.GetCategory(id, trackChanges) ?? throw new CategoryNotFoundException(id);

            var categoryDto = _mapper.Map<CategoryDto>(category);
            return categoryDto;
        }
    }
 }
    