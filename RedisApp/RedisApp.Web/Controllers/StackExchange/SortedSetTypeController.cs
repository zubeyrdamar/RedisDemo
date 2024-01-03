using Microsoft.AspNetCore.Mvc;
using RedisApp.Web.Services;

namespace RedisApp.Web.Controllers.StackExchange
{
    public class SortedSetTypeController : BaseController
    {
        public string Key { get; set; } = "SortedSetKey";

        /*
        |
        |   We inherit BaseController to be able to
        |   access Redis from one source
        |
        */

        public SortedSetTypeController(RedisService redisService) : base(redisService)
        {

        }

        public IActionResult Set()
        {
            return View("~/Views/StackExchange/SortedSetType/Set.cshtml");
        }

        public IActionResult Get()
        {
            /*
            |
            |   Create an empty hash   
            |
            */

            HashSet<string> hash = new HashSet<string>();

            /*
            |
            |   Check if any hash exists with the key in Redis
            |   & fetch data
            |
            */

            if (database.KeyExists(Key))
            {
                database.SortedSetScan(Key).ToList().ForEach(i =>
                {
                    hash.Add(i.ToString());
                });
            }

            ViewBag.Hash = hash;

            return View("~/Views/StackExchange/SortedSetType/Get.cshtml");
        }

        public IActionResult Add(string data, int index)
        {
            /*
            |
            |   Add data to sorted hash
            |
            */

            database.SortedSetAdd(Key, data, index);

            return RedirectToAction("Get", "SortedSetType");
        }

        public IActionResult Delete(string data)
        {
            /*
            |
            |   Remove item with given data from the sorted hash
            |
            */

            database.SortedSetRemoveAsync(Key, data).Wait();

            return RedirectToAction("Get", "SortedSetType");
        }
    }
}
