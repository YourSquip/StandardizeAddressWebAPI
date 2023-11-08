using Microsoft.AspNetCore.Mvc;
using Dadata.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StandardizeAddressWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        
        private readonly ILogger<ServiceController> _logger;
        private readonly IHttpClientFactory _httpClientFactory; // httpclient creates in get 
        public ServiceController(ILogger<ServiceController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;

        }
        //using HttpClient client = _httpClientFactory.CreateClient();
        //public 
        // GET: api/<ServiceController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            using HttpClient client = _httpClientFactory.CreateClient();
            return new string[] { "value1", "value2" };
        }
        //public async Task GetAsync()
        //{
        //    using HttpClient client = _httpClientFactory.CreateClient();
        //    using HttpResponseMessage response = await client.GetAsync("/WeatherForecast");
        //    var jsonResponse = await response.Content.ReadAsStringAsync();
        //    Console.WriteLine(jsonResponse);
        //    //response.EnsureSuccessStatusCode()
        //}
        //public IEnumerable<string> Get()
        //{

        //    return new string[] { "value1", "value2" };
        //}

    }
}
