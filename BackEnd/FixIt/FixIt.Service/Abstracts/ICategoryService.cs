using FixIt.Domain.Entities;

namespace FixIt.Service.Abstracts
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategoriesAsync();
        Task<string> AddCategoryAsync(Category category);
        Task<string> UpdateCategoryAsync(Category category);
        Task<string> DeleteCategoryAsync(Category category);
        Task<Category> GetCategoryByIdAsync(int id);
        Task<Category> GetCategoryByNameAsync(string name);

    }
}
