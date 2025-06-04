using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using ProductCrudApplication.Data;
using ProductCrudApplication.DTO;
using ProductCrudApplication.Model;

namespace ProductCrudApplication.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
        }

        public void DeleteProduct(int productId)
        {
            var category = _context.Products.Find(productId);
            if (category != null)
            {
                _context.Products.Remove(category);
            }
        }

        public ProductListDTOClass GetAllProducts(int pageNumber = 1, int pageSize = 10)
        {
            int skip = (pageNumber - 1) * pageSize;

            var products = _context.Products
                .Skip(skip)
                .Take(pageSize)
                .Select(p => new ProductDTO
                {
                    ProductId =p.ProductId,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    CategoryId = p.CategoryId,
                    CategoryName = _context.Categories
                        .Where(c => c.CategoryId == p.CategoryId)
                        .Select(c => c.Name)
                        .FirstOrDefault()
                })
                .ToList();
            int count = _context.Products.Count();
            int total = (int)Math.Ceiling((double)count / pageSize);

            return new ProductListDTOClass { Products = products, TotalCount = count, CurrentPage=pageNumber, TotalPage = total };
        }

        public Object GetProductById(int productId)
        {
            var product = _context.Products
                  .Where(p => p.ProductId == productId)
                  .Select(p => new
                  {
                      p.ProductId,
                      p.Name,
                      p.Description,
                      p.Price,
                      p.CategoryId,
                      CategoryName = _context.Categories
                        .Where(c => c.CategoryId == p.CategoryId)
                        .Select(c => c.Name)
                        .FirstOrDefault() // Get the category name manually
                  }).FirstOrDefault();

            return product;
        }

        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
