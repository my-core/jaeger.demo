using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Jaeger.BService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        //[HttpGet]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    var rng = new Random();
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = rng.Next(-20, 55),
        //        Summary = Summaries[rng.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}
        
        public async Task<Dictionary<string, object>> Get()
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            using (var conn = new SqlConnection("Data Source=192.168.3.112;Initial Catalog=NZ_Yunying;User ID=sa;Password=taotao778899!;Persist Security Info=False;Max Pool Size=500"))
            {
                var feedList = await conn.QueryAsync<NewsFeedInfo>("select * from NewsFeeds");
                dic.Add("feedList", feedList.AsList());
            }
            using (var conn = new MySqlConnection("Server=127.0.0.1;Database=test;Uid=root;Pwd=;port=3306;pooling=true;charset=utf8;SslMode=none"))
            {
                var userList = await conn.QueryAsync<UserInfo>("select * from user");
                dic.Add("userList", userList.AsList());
            }
            return dic;
        }

        public class NewsFeedInfo
        {
            public int Id { get; set; }
            public string NewsFeedName { get; set; }
        }

        public class UserInfo
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Sex { get; set; }
            public int Age { get; set; }
        }
    }
}
