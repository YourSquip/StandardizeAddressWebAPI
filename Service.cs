using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Dadata;
using Dadata.Model;
using Microsoft.AspNetCore.DataProtection;
using static System.Net.Mime.MediaTypeNames;

namespace StandardizeAddressWebAPI
{
    public class Service
    {

        private readonly IHttpClientFactory _httpClientFactory = null!;
        private readonly IConfiguration _configuration = null!;
        private readonly ILogger<Service> _logger = null!;

        public Service(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            ILogger<Service> logger) =>
            (_httpClientFactory, _configuration, _logger) =
                (httpClientFactory, configuration, logger);
        public async Task CreateItemAsync(AddressDto addressDto)
        {
            var addressJson = new StringContent(
                JsonSerializer.Serialize(addressDto),
                Encoding.UTF8,
                Application.Json); // using static System.Net.Mime.MediaTypeNames;

            string? httpClientDadata = _configuration["HttpClientDadata"];
            using HttpClient client = _httpClientFactory.CreateClient(httpClientDadata ?? "");

            using var httpResponseMessage =
                await client.PostAsync("/api/Address", addressJson);

            httpResponseMessage.EnsureSuccessStatusCode();
        }
        //public async Task<Address> GetAddressAsync(int userId)
        //{
        //    // Create the client
        //    string? httpClientName = _configuration["HttpClientName"];
        //    using HttpClient client = _httpClientFactory.CreateClient(httpClientName ?? "");

        //    try
        //    {
        //        // Make HTTP GET request
        //        // Parse JSON response deserialize into Todo type
        //        Address? address = await client.GetFromJsonAsync<Address>(
        //            $"todos?userId={userId}",
        //            new JsonSerializerOptions(JsonSerializerDefaults.Web));

        //        return address;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Error getting something fun to say: {Error}", ex);
        //    }

        //    return Array.Empty<Todo>();
        //}
    }
    //public class Service
    //{
    //    private readonly IHttpClientFactory _httpClientFactory;

    //    public Service(IHttpClientFactory httpClientFactory)
    //    {
    //        _httpClientFactory = httpClientFactory;
    //        //_httpClient = httpClient;
    //        //_httpClient.BaseAddress = new Uri("https://cleaner.dadata.ru/api/v1/clean/address");
    //    }
    //    public async Task OnGet
    //    //public Service(IHttpClientFactory) { }
    //}
}
