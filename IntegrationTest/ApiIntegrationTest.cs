using FAS.API;
using FAS.Model;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

namespace IntegrationTest
{
    public class ApiIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        public ApiIntegrationTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }
        [Fact]
        public async Task Test1()
        {
            var client = _factory.CreateClient();

            var response = await client.GetAsync("/api/Product/0");
            response.StatusCode.Equals(HttpStatusCode.NotFound);

            var product = new Product
            {
                Name = "Test Product",
                Price = 6.66M
            };

            // Act: Create the product via API
            var createResponse = await client.PostAsJsonAsync("/api/Product", product);
            createResponse.EnsureSuccessStatusCode();

            // Read content from the response
            var createdProduct = await createResponse.Content.ReadFromJsonAsync<Product>();
            Assert.NotNull(createdProduct);
            Assert.Equal("Test Product", createdProduct.Name);
            Assert.Equal(6.66M, createdProduct.Price);

        }
    }
}