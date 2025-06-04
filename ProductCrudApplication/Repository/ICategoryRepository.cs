using ProductCrudApplication.Model;

namespace ProductCrudApplication.Repository
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAllCategories(int pageNumber = 1, int pageSize = 10);
        Category AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(int categoryId);
        Category GetCategoryById(int categoryId);
        void SaveChanges();
    }
}
