using ProductCrudApplication.DTO;
using ProductCrudApplication.Model;

namespace ProductCrudApplication.Service
{
    public interface IProductService
    {
        ProductListDTOClass GetAllProducts(int pageNumber = 1, int pageSize = 10);
        Object GetProductById(int productId);
        Product AddProduct(Product product);
        void DeleteCategory(int productId);
        void UpdateProduct(Product product);
    }
}
