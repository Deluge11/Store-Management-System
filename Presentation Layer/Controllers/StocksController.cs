using Microsoft.AspNetCore.Mvc;
using Business_Layer.Interfaces;
using DTOs;
using Microsoft.AspNetCore.Authorization;

namespace Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StocksController : ControllerBase
    {

        public IStocksBusiness StocksBusiness { get; }

        public StocksController(IStocksBusiness stocksBusiness)
        {
            StocksBusiness = stocksBusiness;
        }

        [HttpPost("add-stock")]
        public async Task<IActionResult> Add(AddNewStock stock)
        {
            return await StocksBusiness.Add(stock) ?
              Ok() : BadRequest();
        }

        [HttpPost("add-quantity")]
        public async Task<IActionResult> AddStockQuantity(InsertProduct stock)
        {
            return await StocksBusiness.AddStockQuantityRequest(stock) ?
              Ok() : BadRequest();
        }

        [HttpPost("{requestId}")]
        public async Task<IActionResult> AcceptAddStockReqeuest(int requestId)
        {
            return await StocksBusiness.AcceptAddStockReqeuest(requestId) ?
              Ok() : BadRequest();
        }


    }
}
