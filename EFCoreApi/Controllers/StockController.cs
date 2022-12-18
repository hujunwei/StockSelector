using Integration;
using Microsoft.AspNetCore.Mvc;

// TODO: AuthN/AuthZ
namespace EFCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class StocksController : ControllerBase
    {
        private readonly ILogger<StocksController> _logger;

        public StocksController(ILogger<StocksController> logger)
        {
            _logger = logger;
        }

        // GET: api/Stocks
        [HttpGet]
        public async Task<ResponseData> GetStocksList()
        {
            using var httpClient = new HttpClient();
            var tuShareClient = new TuShareClient(httpClient);
            var data = await tuShareClient.ListAllStocks();

            return data;
        }
    }
}
