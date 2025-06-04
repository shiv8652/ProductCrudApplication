using ProductCrudApplication.Model;

namespace ProductCrudApplication.Service
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAllCategories(int pageNumber = 1, int pageSize = 10);
        Category GetCategoryById(int categoryId);
        Category AddCategory(Category category);
        void DeleteCategory(int categoryId);
        void UpdateCategory(Category category);
    }
}
