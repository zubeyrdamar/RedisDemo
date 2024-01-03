using Microsoft.AspNetCore.Mvc;
using RedisApp.Web.Services;

namespace RedisApp.Web.Controllers.StackExchange
{
    public class SetTypeController : BaseController
    {
        public string Key { get; set; } = "SetKey";

        /*
        |
        |   We inherit BaseController to be able to
        |   access Redis from one source
        |
        */

        public SetTypeController(RedisService redisService) : base(redisService)
        {

        }

        public IActionResult Set()
        {
            return View("~/Views/StackExchange/SetType/Set.cshtml");
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
                database.SetMembers(Key).ToList().ForEach(i =>
                {
                    hash.Add(i.ToString());
                });
            }

            ViewBag.Hash = hash;

            return View("~/Views/StackExchange/SetType/Get.cshtml");
        }

        public IActionResult Add(string data) 
        {
            /*
            |
            |   Determine expiration date for data
            |
            */

            database.KeyExpire(Key, DateTime.Now.AddMinutes(5));

            /*
            |
            |   Add data to hash
            |
            */

            database.SetAdd(Key, data);

            return RedirectToAction("Get", "SetType");
        }

        public IActionResult Delete(string data)
        {
            /*
            |
            |   Remove item with given data from the hash
            |
            */

            database.SetRemoveAsync(Key, data).Wait();

            return RedirectToAction("Get", "SetType");
        }
    }
}
