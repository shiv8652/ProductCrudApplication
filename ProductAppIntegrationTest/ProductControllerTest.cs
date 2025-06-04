using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using ProductCrudApplication.Model;
using ProductCrudApplication.DTO;
using Newtonsoft.Json;

namespace ProductAppIntegrationTest
{
    public class ProductControllerTest : BaseIntegrationTest
    {
        public ProductControllerTest(WebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        public async Task GetAllProducts_ReturnListOfProduct()
        {
            var response = await _client.GetAsync("/api/product");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<ProductListDTOClass>(content);
            Assert.NotNull(products);
            Assert.True(products.Products.Count > 0);
        }

        [Fact]
        public async Task GetProductById_ReturnProductById()
        {
            var response = await _client.GetAsync("/api/product/1");
            response.EnsureSuccessStatusCode();
            var product = await response.Content.ReadFromJsonAsync<Product>();
            Assert.NotNull(product);
            Assert.Equal("Laptop", product.Name);
        }

        [Fact]
        public async Task DeleteProduct_ReturnDeletedSuccessfullMessage()
        {
            var response = await _client.DeleteAsync("/api/product/2");
            response.EnsureSuccessStatusCode();
            var res = await response.Content.ReadAsStringAsync();
            Assert.NotNull(response);
            Assert.Equal("Product deleted successfuly", res);
        }

        [Fact]
        public async Task UpdateProduct_ReturnUpdatedSuccessfullMessage()
        {
            var productToUpdate = new Product { ProductId=3,Name="Rare Rabbit",Price=123,CategoryId=1};
            var response = await _client.PutAsJsonAsync("/api/product", productToUpdate);
            response.EnsureSuccessStatusCode();
            var res = await response.Content.ReadAsStringAsync();
            Assert.NotNull(response);
            Assert.Equal("Product updated successfuly", res);
        }

        [Fact]
        public async Task CreateProduct_ReturnCraetedProduct()
        {
            var productToCreate = new Product { Name = "Iphone 15",Price=123,CategoryId=1};
            var response = await _client.PostAsJsonAsync("/api/product", productToCreate);
            response.EnsureSuccessStatusCode();
            var res = await response.Content.ReadFromJsonAsync<Product>();
            Assert.NotNull(response);
            Assert.Equal("Iphone 15", res.Name);
        }
    }
}
