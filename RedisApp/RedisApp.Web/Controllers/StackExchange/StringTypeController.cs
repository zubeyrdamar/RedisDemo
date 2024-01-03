using Microsoft.AspNetCore.Mvc;
using RedisApp.Web.Services;

namespace RedisApp.Web.Controllers.StackExchange
{
    public class StringTypeController : BaseController
    {
        /*
        |
        |   We inherit BaseController to be able to
        |   access Redis from one source
        |
        */

        public StringTypeController(RedisService redisService) : base(redisService)
        {

        }

        public IActionResult Set()
        {
            /*
            |
            |   Set data in Redis database
            |
            */

            database.StringSet("name", "Joe Biden");
            database.StringSet("visitor", 0);
            
            return View("~/Views/StackExchange/StringType/Set.cshtml");
        }

        public IActionResult Get()
        {
            /*
            |
            |   Get data from Redis database
            |
            */

            var name = database.StringGet("name");
            var visitor = database.StringIncrementAsync("visitor", 1).Result; // increase visitor by 1

            /*
            |
            |   Check if fetched data is not null
            |
            */

            if (name.HasValue)
            {
                ViewBag.Name = name.ToString();
                ViewBag.Visitor = visitor.ToString();
            }

            return View("~/Views/StackExchange/StringType/Get.cshtml");
        }
    }
}
