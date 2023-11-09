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

    }  
}
