using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using RedisApp.Web.Models;
using System.Text;

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

        public IActionResult SetFile()
        {
            /*
            |
            |   To save files in redis we need to convert the image
            |   to a byte variable.
            |
            |   First, we need to get image. In this example we use
            |   static image file and take its path.
            |
            */

            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/redis.png");

            /*
            |
            |   Then we convert the image to byte
            |
            */

            byte[] imageByte = System.IO.File.ReadAllBytes(path);

            /*
            |
            |   Then we should be able to cache image in Redis
            |
            */

            distributedCache.Set("img", imageByte);

            return View();
        }

        public IActionResult GetFile()
        {
            /*
            |
            |   To read files from redis we need to convert byte
            |   to an image file
            |
            */

            byte[] imageByte = distributedCache.Get("img") ?? [];
            
            return File(imageByte, "image/png");
        }
    }
}
