using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Jaeger.AService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        

        private readonly ILogger<TestController> _logger;
        private IHttpClientFactory _httpClientFactory;

        public TestController(ILogger<TestController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> GetFromBServiceAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var body = await client.GetStringAsync("http://127.0.0.1:5001/api/test");
            return body;
        }
    }
}
