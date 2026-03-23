using FixIt.Domain.Entities;
using FixIt.Infrastructure.Abstracts;
using FixIt.Service.Abstracts;

namespace FixIt.Service.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoryService(ICategoryRepository categoryRepo)
        {

            _categoryRepo = categoryRepo;
        }

        public async Task<string> AddCategoryAsync(Category category)
        {
            await _categoryRepo.AddAsync(category);
            return "success";
        }

        public async Task<string> DeleteCategoryAsync(Category category)
        {
            await _categoryRepo.DeleteAsync(category);
            return "success";
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {

            var CategoriesList = await _categoryRepo.GetAllAsync();
            return CategoriesList;

        }

        public Task<Category> GetCategoryByIdAsync(int id)
        {
            return _categoryRepo.GetByIdAsync(id);
        }

        public async Task<Category> GetCategoryByNameAsync(string name)
        {
            return await _categoryRepo.GetCategoryByNameAsync(name);
        }

        public async Task<string> UpdateCategoryAsync(Category category)
        {
            await _categoryRepo.UpdateAsync(category);
            return "success";
        }
    }
}
