using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;
using Dadata;
using Dadata.Model;
using AutoMapper;
using AutoMapper;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Components.RenderTree;
using System.Net.Http;
using System.Text;
using System.Reflection.PortableExecutable;


//So the main idea was to form a POST request to dadata get that json and return it in GET request
//But I wasn't able to send correct POST request to dadata (for some reason)(actually that was 400 failed to read request)
//But I setted up everything else so you can look up at it separately through POST and GET requests in OPENApi
//
namespace StandardizeAddressWebAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : Controller
    {
        
        static HttpClient _httpClient;

        public string roughAddress = "мск сухонска 11/-89";
        private readonly Mapper _mapper;


        private readonly IConfiguration _configuration = null!;
        private readonly ILogger<ServiceController> _logger = null!;
        private readonly IHttpClientFactory _httpClientFactory = null!;
       
        public ServiceController(ILogger<ServiceController> logger,
                                IConfiguration configuration, 
                                IHttpClientFactory httpClientFactory)
        {
            _logger = logger;

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Address, AddressDto>());
            _mapper = new Mapper(config);

            var builder = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    { "token","927777ed1d86fc93ea4f121c89e662d7174b3d21" },
                    { "secret", "caac29a684135a2279bda141485fc222258a2b8e" },
                    { "dadata_url", "https://cleaner.dadata.ru/api/v1/clean/address" }
                });
            _configuration = builder.Build();
            _httpClientFactory = httpClientFactory;
            string? httpClientDadata = _configuration["dadata_url"];
            _httpClient = _httpClientFactory.CreateClient(httpClientDadata ?? "");

        }
        [HttpGet]
        public async Task<JsonResult> Get()
        {
            var token = _configuration["token"];
            var secret = _configuration["secret"];
            var api = new CleanClientAsync(token, secret);

            //let's pretend it's coming back in form of json and we deserialized it, srry
            var fixedAddress = await api.Clean<Address>(roughAddress);
            //mapps Address to AddressDto
            var addressDto = _mapper.Map<Address, AddressDto>(fixedAddress);
            var settings = new JsonSerializerSettings { };
           
            //return json
            return new JsonResult(addressDto);
        }
        [HttpPost] //returns Bad Request 400, I don't know what's wrong
        public async Task<JsonResult> Post([FromBody] string address)
        {
            address = roughAddress;

            //making post request
            using var request = new HttpRequestMessage(HttpMethod.Post, _configuration["dadata_url"]);
            //add needed headers for authorization 
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Authorization", "Token 927777ed1d86fc93ea4f121c89e662d7174b3d21");
            request.Headers.Add("X-Secret", "caac29a684135a2279bda141485fc222258a2b8e");
            //making string content for sending
            StringContent content = new StringContent("мск сухонска 11/-89", Encoding.UTF8, "application/json");
            request.Content = content;
            //using var response = await httpClient.PostAsync(_configuration["dadata_url"],content);
            using var response = await _httpClient.SendAsync(request);
            string responseText = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseText);
            //getting back our 
            return new JsonResult(responseText);
        }
       
    }
}
