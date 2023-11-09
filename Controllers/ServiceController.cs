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


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860



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
            //
            //_httpClient = httpClient;
            //_httpClient.BaseAddress = new Uri("https://cleaner.dadata.ru/api/v1/clean/address");

        }
        [HttpGet]
        public async Task<JsonResult> Get()
        {
            var token = "927777ed1d86fc93ea4f121c89e662d7174b3d21";
            var secret = "caac29a684135a2279bda141485fc222258a2b8e";
            var api = new CleanClientAsync(token, secret);

            //let's pretend it's coming back in form of json
            var fixedAddress = await api.Clean<Address>(roughAddress);//используем dadata, чтобы стандартизировать адрес
            var addressDto = _mapper.Map<Address, AddressDto>(fixedAddress);
            var settings = new JsonSerializerSettings { };
           
            return new JsonResult(addressDto);
        }
        [HttpPost] //return Bad Request 400 
        public async Task<JsonResult> Post([FromBody] string address)
        {
            address = roughAddress;
            using var request = new HttpRequestMessage(HttpMethod.Post, _configuration["dadata_url"]);

            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("Authorization", "Token 927777ed1d86fc93ea4f121c89e662d7174b3d21");
            request.Headers.Add("X-Secret", "caac29a684135a2279bda141485fc222258a2b8e");
            
            StringContent content = new StringContent("мск сухонска 11/-89", Encoding.UTF8, "application/json");
            request.Content = content;
            //using var response = await httpClient.PostAsync(_configuration["dadata_url"],content);
            using var response = await _httpClient.SendAsync(request);
            string responseText = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseText);
            return new JsonResult(responseText);
        }
       
    }
}
