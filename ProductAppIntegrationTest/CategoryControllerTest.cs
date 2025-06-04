using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using Microsoft.AspNetCore.Mvc.Testing;
using ProductCrudApplication.Model;
using System.Net.Http.Json;

namespace ProductAppIntegrationTest
{
    public class CategoryControllerTest : BaseIntegrationTest
    {

        public CategoryControllerTest(WebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        public async Task GetAllCategories_ReturnListOfCategory()
        {
            var response = await _client.GetAsync("/api/category");
            response.EnsureSuccessStatusCode();
            var categories = await response.Content.ReadFromJsonAsync<List<Category>>();
            Assert.NotNull(categories);
            Assert.True(categories.Count > 0);
        }

        [Fact]
        public async Task GetCategoryById_ReturnCategoryById()
        {
            var response = await _client.GetAsync("/api/category/1");
            response.EnsureSuccessStatusCode();
            var category = await response.Content.ReadFromJsonAsync<Category>();
            Assert.NotNull(category);
            Assert.Equal("Electronics", category.Name);
        }

        [Fact]
        public async Task DeleteCategory_ReturnDeletedSuccessfullMessage()
        {
            var response = await _client.DeleteAsync("/api/category/2");
            response.EnsureSuccessStatusCode();
            var res = await response.Content.ReadAsStringAsync();
            Assert.NotNull(response);
            Assert.Equal("Category deleted successfuly", res);
        }

        [Fact]
         public async Task UpdateCategory_ReturnUpdatedSuccessfullMessage()
        {
            var categoryToUpdate = new Category { CategoryId = 3, Name = "Updated Books"};
            var response = await _client.PutAsJsonAsync("/api/category", categoryToUpdate);
            response.EnsureSuccessStatusCode();
            var res = await response.Content.ReadAsStringAsync();
            Assert.NotNull(response);
            Assert.Equal("Category updated successfuly", res);
        }

        [Fact]
        public async Task CreateCategory_ReturnCraetedCategory()
        {
            var categoryToCreate = new Category { Name = "Sports",Description="sports category" };
            var response = await _client.PostAsJsonAsync("/api/category", categoryToCreate);
            response.EnsureSuccessStatusCode();
            var res = await response.Content.ReadFromJsonAsync<Category>();
            Assert.NotNull(response);
            Assert.Equal("Sports", res.Name);
        }

    }
}
