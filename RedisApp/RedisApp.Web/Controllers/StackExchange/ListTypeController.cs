using Microsoft.AspNetCore.Mvc;
using RedisApp.Web.Services;

namespace RedisApp.Web.Controllers.StackExchange
{
    public class ListTypeController : BaseController
    {
        public string Key { get; set; } = "ListKey";

        /*
        |
        |   We inherit BaseController to be able to
        |   access Redis from one source
        |
        */

        public ListTypeController(RedisService redisService) : base(redisService)
        {

        }

        public IActionResult Set()
        {
            return View("~/Views/StackExchange/ListType/Set.cshtml");
        }

        public IActionResult Get()
        {
            /*
            |
            |   Create an empty list   
            |
            */

            List<string> list = new List<string>();

            /*
            |
            |   Check if any list exists with the key in Redis
            |   & fetch data
            |
            */

            if (database.KeyExists(Key))
            {
                database.ListRange(Key).ToList().ForEach(i =>
                {
                    list.Add(i.ToString());
                });
            }

            ViewBag.List = list;

            return View("~/Views/StackExchange/ListType/Get.cshtml");
        }

        [HttpPost]
        public IActionResult Add(string data)
        {
            /*
            |
            |   Push data to end (right) of the list
            |
            |   To push data in the begining of the list we would use left push
            |
            */

            database.ListRightPush(Key, data);

            return RedirectToAction("Get", "ListType");
        }

        public IActionResult Delete(string data)
        {
            /*
            |
            |   Remove item with given data from the list
            |
            */

            database.ListRemoveAsync(Key, data).Wait();

            return RedirectToAction("Get", "ListType");
        }
    }
}
