using CustomerListt.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomerListt.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KcbController : Controller
    {
        private readonly KcbService _kcbService;

        public KcbController(KcbService kcbService)
        {
            _kcbService = kcbService;
        }

        [HttpGet("token")]
        public async Task<IActionResult> GetToken()
        {
            string username = "aeOE_tylXOkazaEe7VLtaNI6nxoa";
            string password = "_MDx8kAsXyhtT7Ekyj1vlZfsLEYa";

            var tokenResponse = await _kcbService.FetchTokenAsync(username, password);
            return Content(tokenResponse, "application/json");
        }
    }
}
