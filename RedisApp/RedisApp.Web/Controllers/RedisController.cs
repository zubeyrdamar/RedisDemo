using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RedisApp.Web.Models;

namespace RedisApp.Web.Controllers
{
    public class RedisController : Controller
    {
        /*
        |
        |   We need to add Distributed Cache instace
        |   to use Redis operations
        |
        */

        private readonly IDistributedCache distributedCache;
        public RedisController(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;
        }

        public IActionResult Set()
        {
            /*
            |
            |   We may define expiration or priority options
            |   Process is the same as Memory Cache
            |
            */

            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions();
            options.AbsoluteExpiration = DateTime.Now.AddSeconds(30);

            /*
            |
            |   When we need to cache complex data in Redis
            |   it is an obligation to handle the convertion
            |   by ourselves (In Memory Cache no need to convert).
            |
            */

            RedisDataViewModel model = new()
            {
                Id = 1,
                Name = "test",
                Description = "test data description",
            };

            string jsonRedisData = JsonConvert.SerializeObject(model);

            /*
            |
            |   Set data in Redis (Distributed Cache)
            |
            */

            distributedCache.SetStringAsync("data:1", jsonRedisData, options);

            return View();
        }

        public IActionResult Get()
        {

            /*
            |
            |   Get data from Redis (Distributed Cache)
            |
            */

            ViewBag.Data = distributedCache.GetString("name");

            /*
            |
            |   Remove data from Redis (Distributed Cache)
            |
            */

            distributedCache.Remove("name");

            return View();
        }
    }
}
