using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace RedisApp.Web.Controllers
{
    public class MemoryCacheController : Controller
    {
        private readonly IMemoryCache memoryCache;
        public MemoryCacheController(IMemoryCache memoryCache)
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

                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();

                /*
                |
                |   When we cache a data, we also want to set an expiration time
                |   for that data. Generally we do not want to get the same data
                |   all the time.
                |
                |   Absolute Expiration: We define an exact time that data will be removed
                |   Sliding Expiration: We define a period that data will be removed if
                |                       data is not got in this time.
                |
                */

                options.AbsoluteExpiration = DateTime.Now.AddSeconds(30);
                options.SlidingExpiration = TimeSpan.FromSeconds(10);

                /*
                |
                |   When we cache our data we also can define the priority.
                |   Priority matters when memory is full. When the memory is full,
                |   we want to remove some of the cached data & this removal will
                |   be done according to priority. Low priority data is removed first
                |   & High priority data is removed last.
                |
                |   We have to be careful when using NeverRemove priority.
                |   Because even though memory is full we cannot delete data in this
                |   priority. This may throw an exception.
                |
                */

                options.Priority = CacheItemPriority.Low;
                options.Priority = CacheItemPriority.Normal;
                options.Priority = CacheItemPriority.High;
                options.Priority = CacheItemPriority.NeverRemove;

                /*
                |
                |   When a cached data is removed we may want to know the reason why
                |   data is removed. To get that information we call RegisterPostEvictionCallback
                |   method.
                |
                */

                options.RegisterPostEvictionCallback((key, value, reason, state) =>
                {
                    memoryCache.Set("callback", $"{key}:{value} is removed --- reason:{reason}");
                });

                memoryCache.Set<string>("time", DateTime.Now.ToString(), options);
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
            */

            memoryCache.Remove("time");

            return View();
        }
    }
}
