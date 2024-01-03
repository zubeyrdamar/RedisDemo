using StackExchange.Redis;

namespace RedisApp.Web.Services
{
    public class RedisService
    {
        private ConnectionMultiplexer redis;
        private readonly string redisHost;
        private readonly string redisPort;

        public IDatabase database { get; set; }

        /*
        |
        |   To be able to create a Redis connection
        |   we need to have host & port information.
        |   This information is defined in appsettings.json
        |   file. In the constructor we get those data
        |   using Configuration.
        |
        */

        public RedisService(IConfiguration configuration)
        {
            redisHost = configuration["Redis:Host"] ?? "localhost";
            redisPort = configuration["Redis:Port"] ?? "6379";

            Connect();
        }

        /*
        |
        |   We need to create a connection to Redis server
        |
        */

        public void Connect()
        { 
            /*
            |
            |   Set connection string
            |
            */

            var configString = $"{redisHost}:{redisPort}";

            /*
            |
            |   Get redis connection
            |
            */

            redis = ConnectionMultiplexer.Connect(configString);
        }

        /*
        |
        |   Redis provides 16 different databases for several
        |   purposes (testing, local, live etc.)
        |
        |   To get the database we would like to work on, we 
        |   need to get the database with giving the id of db.
        |
        */

        public IDatabase GetRedisDatabase(int database)
        {
            return redis.GetDatabase(database);
        }
    }
}
