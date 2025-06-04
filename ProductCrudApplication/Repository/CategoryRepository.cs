using Microsoft.EntityFrameworkCore;
using ProductCrudApplication.Data;
using ProductCrudApplication.Model;

namespace ProductCrudApplication.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public Category AddCategory(Category category)
        {
           _context.Categories.Add(category);
            return category;
        }

        public void DeleteCategory(int categoryId)
        {
            var product = _context.Categories.Find(categoryId);
            if (product != null) 
            {
                _context.Categories.Remove(product);
            }
        }

        public IEnumerable<Category> GetAllCategories(int pageNumber = 1, int pageSize = 10)
        {
            int skip = (pageNumber - 1) * pageSize;

            var categories = _context.Categories
                .Skip(skip)
                .Take(pageSize)
                .Include(c => c.Products).AsNoTracking()
                .ToList();
            return categories;
        }

        public Category GetCategoryById(int categoryId)
        {
            var category = _context.Categories.AsNoTracking()
                .Include(c => c.Products).FirstOrDefault(c => c.CategoryId == categoryId);
            if (category == null)
            {
                throw new KeyNotFoundException("Category not found");
            }

            return category;
        }

        public void UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
