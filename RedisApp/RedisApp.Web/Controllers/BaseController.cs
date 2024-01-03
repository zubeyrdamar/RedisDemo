using Microsoft.AspNetCore.Mvc;
using RedisApp.Web.Services;
using StackExchange.Redis;

namespace RedisApp.Web.Controllers
{
    /*
    |
    |   We better create a base controller to handle 
    |   common methods & instances like Redis (or auth user etc.)
    |
    |   Controllers which use Redis should inherit
    |   this BaseController & access Redis via this Controller
    |   
    */

    public class BaseController : Controller
    {
        private readonly RedisService _redisService;
        protected readonly IDatabase database;

        public BaseController(RedisService redisService)
        {
            _redisService = redisService;

            // Get database with index 0
            database = _redisService.GetRedisDatabase(0);
        }
    }
}
