using ProductCrudApplication.DTO;
using ProductCrudApplication.Model;

namespace ProductCrudApplication.Repository
{
    public interface IProductRepository
    {
        ProductListDTOClass GetAllProducts(int pageNumber = 1, int pageSize = 10);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int productId);
        Object GetProductById(int productId);
        void SaveChanges();
    }
}
