using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace RedisApp.Web.Controllers
{
    public class TimeController : Controller
    {
        private readonly IMemoryCache memoryCache;
        public TimeController(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public IActionResult Set()
        {
            /* 
            |
            |   We may want to add or update cache data.
            |   Before doing that it is a good idea to check if ther is
            |   any data with the key which we are trying to assign data.
            |
            |   In this situation the time is updated if it is not cached
            |   or the old data is removed from the cache.
            |
            |   TryGetValue : tries to find data with the key
            |                 if data exists then it returns true,
            |                 if data does not exist then it returns false
            |
            */

            if (!memoryCache.TryGetValue("time", out _))
            {
                memoryCache.Set<string>("time", DateTime.Now.ToString());
            }

            return View();
        }

        public IActionResult Get()
        {
            /* 
            |
            |   We may try to get data with the given key.
            |   If there is no data found in the cache with the given
            |   key, then we should handle the situation.
            |
            |   GetOrCreate : if data does not exist with the given key
            |                 then we assign data to the key.
            |                 This prevents us from errors.
            |
            */

            memoryCache.GetOrCreate<string>("time", entry =>
            {
                return DateTime.Now.ToString();
            });

            /*
            |
            |   Get : gets data with the given key
            |
            */

            ViewBag.Time = memoryCache.Get<string>("time");

            /*
            |
            |   Remove : removes data with the given key
            |
            |   In this example time is removed after caching.
            |
            */

            memoryCache.Remove("time");

            return View();
        }
    }
}
