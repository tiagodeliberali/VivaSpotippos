using System;
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
        /// <summary>
        /// Given a new property
        /// When post it to properties api rest
        /// Then it should return a successful response
        /// </summary>
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
                    requestData.x = 1;
                    requestData.y = 1;

                    var content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");

                    // Act
                    var response = await client.PostAsync("properties", content);

                    var result = JsonConvert.DeserializeObject<PropertyPostResponse>(await response.Content.ReadAsStringAsync());

                    // Assert
                    Assert.Equal(HttpStatusCode.Created, response.StatusCode);

                    Assert.Equal("0", result.Status);
                    Assert.NotNull(result.CreatedProperty);
                    Assert.True(result.CreatedProperty.id > 0);
                }
            }
        }

        /// <summary>
        /// Given a new property with position already used before
        /// When post it to properties api rest
        /// Then it should return a error response with details about position usage
        /// </summary>
        [Fact]
        public async Task PostPropertyOnAlreadyUsedPosition()
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
                    requestData.x = 1;
                    requestData.y = 1;

                    var content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");

                    await client.PostAsync("properties", content);

                    // Act
                    content = new StringContent(JsonConvert.SerializeObject(requestData), Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("properties", content);

                    var result = JsonConvert.DeserializeObject<PropertyPostResponse>(await response.Content.ReadAsStringAsync());

                    // Assert
                    string expectedMessage = string.Format(ErrorMessages.PositionAlreadyAllocated, requestData.x, requestData.y);

                    Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

                    Assert.Equal("2", result.Status);
                    Assert.Equal(expectedMessage, result.Message);
                }
            }
        }

        /// <summary>
        /// Given a new property with invalid number of beds
        /// When post it to properties api rest
        /// Then it should return a error response with information about the failure
        /// </summary>
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

        /// <summary>
        /// Given a property id
        /// When get it from properties api rest
        /// Then it should return the property with id and provinces information
        /// </summary>
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

        /// <summary>
        /// Given an invalid property id
        /// When post it to properties api rest
        /// Then it should return null with No Content (204) http status code
        /// </summary>
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
