using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace WebAPIWIthRedis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisController : ControllerBase
    {
        private readonly IDatabase _db;

        public RedisController()
        {
            var redis = ConnectionMultiplexer.Connect("redis");
            _db = redis.GetDatabase();
        }

        [HttpGet("{key}")]
        public async Task<IActionResult> Get(string key)
        {
            var value = await _db.StringGetAsync(key);
            return Ok(value.ToString());
        }

        [HttpPost("{key}")]
        public async Task<IActionResult> Post(string key, [FromBody] string value)
        {
            await _db.StringSetAsync(key, value);
            return Ok();
        }
    }
}
