using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductCrudApplication.DTO;
using ProductCrudApplication.Model;
using ProductCrudApplication.Service;

namespace ProductCrudApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest();
                }
                _productService.AddProduct(product);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateProduct(Product product)
        {
            try
            {
                if (product == null)
                    return BadRequest();

                var existProduct = _productService.GetProductById(product.ProductId);
                if (existProduct == null)
                    return NotFound();

                _productService.UpdateProduct(product);
                return Ok("Product updated successfuly");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{productId}")]
        public IActionResult DeleteProduct(int productId)
        {
            try
            {

                var product = _productService.GetProductById(productId);
                if (product == null)
                    return NotFound();

                _productService.DeleteCategory(productId);
                return Ok("Product deleted successfuly");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{productId}")]
        public IActionResult GetProductById(int productId)
        {
            try
            {

                var product = _productService.GetProductById(productId);
                if (product == null)
                    return NotFound();

                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [ProducesResponseType(typeof(ProductListDTOClass), 200)]
        [HttpGet]
        public ActionResult<ProductListDTOClass> GetAllProducts([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var products = _productService.GetAllProducts(pageNumber, pageSize);
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
