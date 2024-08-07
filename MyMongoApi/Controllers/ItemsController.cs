using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MyMongoApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyMongoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IMongoCollection<Item> _items;
        private readonly IHttpClientFactory _httpClientFactory;

        public ItemController(IMongoDatabase database, IHttpClientFactory httpClientFactory)
        {
            _items = database.GetCollection<Item>("Items");
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<ActionResult<List<Item>>> GetItems()
        {
            var items = await _items.Find(item => true).ToListAsync();
            return Ok(items);
        }

        [HttpPost]
        public async Task<ActionResult> CreateItem(Item item)
        {
            await _items.InsertOneAsync(item);
            return CreatedAtAction(nameof(GetItems), new { id = item.Id }, item);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItem(string id)
        {
            var item = await _items.Find(item => item.Id == id).FirstOrDefaultAsync();
            if (item == null)
                return NotFound();
            return Ok(item);
        }

        [HttpGet("call-another-api")]
        public async Task<ActionResult<string>> CallAnotherApi()
        {
            var client = _httpClientFactory.CreateClient("AnotherApiClient");
            var response = await client.GetAsync("api/greeting");
            response.EnsureSuccessStatusCode();
            var greeting = await response.Content.ReadAsStringAsync();
            return Ok(greeting);
        }
    }
}
