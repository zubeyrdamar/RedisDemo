using Microsoft.AspNetCore.Mvc;
using RedisApp.Web.Services;

namespace RedisApp.Web.Controllers.StackExchange
{
    public class HashTypeController : BaseController
    {

        public string Key { get; set; } = "HashKey";

        /*
        |
        |   We inherit BaseController to be able to
        |   access Redis from one source
        |
        */

        public HashTypeController(RedisService redisService) : base(redisService)
        {

        }

        public IActionResult Set()
        {
            return View("~/Views/StackExchange/HashType/Set.cshtml");
        }

        public IActionResult Get()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            /*
            |
            |   Check database if exists
            |
            */

            if (database.KeyExists(Key))
            {
                /*
                |
                |   Fetch all data from hash
                |
                */

                database.HashGetAll(Key).ToList().ForEach(i => {
                    dictionary.Add(i.Name, i.Value);
                });
            }

            ViewBag.Hash = dictionary;

            return View("~/Views/StackExchange/HashType/Get.cshtml");
        }

        [HttpPost]
        public IActionResult Add(string key, string value)
        {
            /*
            |
            |   Save data in hash with a key
            |
            */

            database.HashSet(Key, key, value);

            return RedirectToAction("Get", "HashType");
        }

        public IActionResult Delete(string data)
        {
            /*
            |
            |   Remove item with given data from the hash
            |
            */

            database.HashDelete(Key, data);

            return RedirectToAction("Get", "ListType");
        }
    }
}
