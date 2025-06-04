using ProductCrudApplication.Model;
using ProductCrudApplication.Repository;

namespace ProductCrudApplication.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public Category AddCategory(Category category)
        {
            _categoryRepository.AddCategory(category);
            _categoryRepository.SaveChanges();
            return category;
        }

        public void DeleteCategory(int categoryId)
        {
            _categoryRepository.DeleteCategory(categoryId);
             _categoryRepository.SaveChanges();
        }

        public IEnumerable<Category> GetAllCategories(int pageNumber = 1, int pageSize = 10)
        {
           return _categoryRepository.GetAllCategories(pageNumber, pageSize);
        }

        public Category GetCategoryById(int categoryId)
        {
            return _categoryRepository.GetCategoryById(categoryId);
        }

        public void UpdateCategory(Category category)
        {
            _categoryRepository.UpdateCategory(category);
            _categoryRepository.SaveChanges();
        }
    }
}
