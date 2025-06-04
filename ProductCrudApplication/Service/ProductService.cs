using ProductCrudApplication.DTO;
using ProductCrudApplication.Model;
using ProductCrudApplication.Repository;

namespace ProductCrudApplication.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Product AddProduct(Product product)
        {
            _productRepository.AddProduct(product);
            _productRepository.SaveChanges();
            return product;
        }

        public void DeleteCategory(int productId)
        {
            _productRepository.DeleteProduct(productId);
            _productRepository.SaveChanges();
        }

        public ProductListDTOClass GetAllProducts(int pageNumber = 1, int pageSize = 10)
        {
            return _productRepository.GetAllProducts(pageNumber, pageSize);
        }

        public object GetProductById(int productId)
        {
            return _productRepository.GetProductById(productId);
        }

        public void UpdateProduct(Product product)
        {
            _productRepository.UpdateProduct(product);
            _productRepository.SaveChanges();
        }
    }
}
