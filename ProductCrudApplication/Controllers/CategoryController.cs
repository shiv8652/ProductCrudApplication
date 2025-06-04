using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductCrudApplication.Model;
using ProductCrudApplication.Service;

namespace ProductCrudApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService) 
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            try
            {
                if (category == null)
                {
                    return BadRequest();
                }
                _categoryService.AddCategory(category);
                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateCategory(Category category)
        {
            try
            {
                if (category == null)
                    return BadRequest();
                
                var existCategory = _categoryService.GetCategoryById(category.CategoryId);
                if (existCategory == null)
                    return NotFound();

                _categoryService.UpdateCategory(category);
                return Ok("Category updated successfuly");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{categoryId}")]
        public IActionResult DeleteCategory(int categoryId)
        {
            try
            {
                var category = _categoryService.GetCategoryById(categoryId);
                if (category == null)
                    return NotFound();

                _categoryService.DeleteCategory(categoryId);
                return Ok("Category deleted successfuly");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{categoryId}")]
        public IActionResult GetCategoryById(int categoryId)
        {
            try
            {

                var category = _categoryService.GetCategoryById(categoryId);
                if (category == null)
                    return NotFound();

                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAllCategories([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var categories = _categoryService.GetAllCategories(pageNumber,pageSize);
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
