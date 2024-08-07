using Microsoft.AspNetCore.Mvc;
using AnotherApi.Services;

namespace AnotherApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GreetingController : ControllerBase
    {
        private readonly IAnotherService _anotherService;

        public GreetingController(IAnotherService anotherService)
        {
            _anotherService = anotherService;
        }

        [HttpGet]
        public ActionResult<string> GetGreeting()
        {
            var greeting = _anotherService.GetGreeting();
            return Ok(greeting);
        }
    }
}
