using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using VivaSpotippos.Model;
using VivaSpotippos.Model.RestEntities;
using VivaSpotippos.Test;
using Xunit;

namespace VivaSpotippos.Integration
{
    public class PropertiesControllerIntegration
    {
        [Fact]
        public async Task PostSuccessful()
        {
            // Arrange
            using (var host = new TestServer(GetWebHostBuilder()))
            {
                using (var client = host.CreateClient())
                {
                    client.DefaultRequestHeaders
                        .Accept
                        .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var requestData = DemoData.ValidPostRequest;

                    var content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");

                    // Act
                    var response = await client.PostAsync("properties", content);

                    var result = JsonConvert.DeserializeObject<PropertyPostResponse>(await response.Content.ReadAsStringAsync());

                    // Assert
                    Assert.Equal(HttpStatusCode.Created, response.StatusCode);

                    Assert.Equal("0", result.Status);
                    Assert.NotNull(result.CreatedProperty);
                    Assert.Equal(1, result.CreatedProperty.id);
                }
            }
        }

        [Fact]
        public async Task PostInvalidProperty()
        {
            // Arrange
            using (var host = new TestServer(GetWebHostBuilder()))
            {
                using (var client = host.CreateClient())
                {
                    client.DefaultRequestHeaders
                        .Accept
                        .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var requestData = DemoData.ValidPostRequest;
                    requestData.beds = 100;

                    var content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");

                    // Act
                    var response = await client.PostAsync("properties", content);

                    var result = JsonConvert.DeserializeObject<PropertyPostResponse>(await response.Content.ReadAsStringAsync());

                    // Assert
                    Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

                    Assert.Equal("1", result.Status);
                    Assert.Contains("'beds'", result.Message);
                    Assert.Null(result.CreatedProperty);
                }
            }
        }

        [Fact]
        public async Task GetSuccessful()
        {
            // Arrange
            using (var host = new TestServer(GetWebHostBuilder()))
            {
                using (var client = host.CreateClient())
                {
                    client.DefaultRequestHeaders
                        .Accept
                        .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    PropertyPostResponse propertyResult = await CreateProperty(client);

                    // Act
                    var response = await client.GetAsync(string.Format("properties/{0}", propertyResult.CreatedProperty.id));

                    var result = JsonConvert.DeserializeObject<Property>(await response.Content.ReadAsStringAsync());

                    // Assert
                    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

                    Assert.NotNull(result);
                    Assert.Equal(propertyResult.CreatedProperty.id, result.id);
                    Assert.Contains("Scavy", result.provinces);
                }
            }
        }

        [Fact]
        public async Task GetInvalidId()
        {
            // Arrange
            using (var host = new TestServer(GetWebHostBuilder()))
            {
                using (var client = host.CreateClient())
                {
                    client.DefaultRequestHeaders
                        .Accept
                        .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    int invalidId = 123;

                    // Act
                    var response = await client.GetAsync(string.Format("properties/{0}", invalidId));

                    var result = JsonConvert.DeserializeObject<Property>(await response.Content.ReadAsStringAsync());

                    // Assert
                    Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

                    Assert.Null(result);
                }
            }
        }

        private static async Task<PropertyPostResponse> CreateProperty(HttpClient client)
        {
            var requestData = DemoData.ValidPostRequest;

            var content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("properties", content);

            var result = JsonConvert.DeserializeObject<PropertyPostResponse>(await response.Content.ReadAsStringAsync());

            return result;
        }

        private static IWebHostBuilder GetWebHostBuilder()
        {
            return new WebHostBuilder()
                            .UseEnvironment("Development")
                            .UseStartup<Startup>();
        }
    }
}
