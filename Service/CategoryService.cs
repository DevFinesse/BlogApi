using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
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

        public async Task<CategoryDto> CreateCategoryAsync(CategoryCreationDto category, bool trackChanges)
        {
            //var categoryEntity = _mapper.Map<Category>(category);
            Category categoryEntity = new()
            {
                Name = category.Name
            };
            _repository.CategoryRepository.CreateCategory(categoryEntity);
            await _repository.SaveAsync();

            CategoryDto categoryToReturn = new()
            {
                Name = categoryEntity.Name
            }; 
            //var categoryToReturn = _mapper.Map<CategoryDto>(categoryEntity);

            return categoryToReturn;
        }

        public async Task DeleteCategoryAsync(Guid categoryId, bool trackChanges)
        {
            var categoryEntity = await _repository.CategoryRepository.GetCategoryAsync(categoryId, trackChanges) ?? throw new CategoryNotFoundException(categoryId);
            _repository.CategoryRepository.DeleteCategory(categoryEntity);
            await _repository.SaveAsync();
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync(bool trackChanges)
        {
                var categories = await _repository.CategoryRepository.GetAllCategoriesAsync(trackChanges);
                var categoriesDto = _mapper.Map<IEnumerable<CategoryDto>>(categories);
                return categoriesDto;
         }

        public async Task<CategoryDto> GetCategoryAsync(Guid id, bool trackChanges)
        { 
            var category = await _repository.CategoryRepository.GetCategoryAsync(id, trackChanges) ?? throw new CategoryNotFoundException(id);
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return categoryDto;
        }

        public async Task UpdateCategoryAsync (Guid categoryId, CategoryUpdateDto category, bool trackChanges)
        {
            var categoryEntity = await _repository.CategoryRepository.GetCategoryAsync(categoryId, trackChanges) ?? throw new CategoryNotFoundException(categoryId);
            var categoryUpdateDto = _mapper.Map(category,categoryEntity);
            await _repository.SaveAsync();
        }
    }
 }
    