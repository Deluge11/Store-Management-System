using Microsoft.AspNetCore.Mvc;
using Business_Layer.Interfaces;
using DTOs;
using Microsoft.AspNetCore.Authorization;
using Enums;
using Presentation_Layer.Authorization;

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


        [Authorize(AuthenticationSchemes = "Ecommerce")]
        [HttpPost("add-stock")]
        public async Task<IActionResult> Add(AddNewStock stock)
        {
            return await StocksBusiness.Add(stock) ?
              Ok() : BadRequest();
        }


        [Authorize(AuthenticationSchemes = "Ecommerce")]
        [HttpPost("add-quantity")]
        public async Task<IActionResult> AddStockQuantity(InsertProduct stock)
        {
            return await StocksBusiness.AddStockQuantityRequest(stock) ?
              Ok() : BadRequest();
        }


        [Authorize]
        [CheckPermission(Permission.Stocks_Accept_Add_Quantity)]
        [HttpPost("{requestId}")]
        public async Task<IActionResult> AcceptAddStockReqeuest(int requestId)
        {
            return await StocksBusiness.AcceptAddStockReqeuest(requestId) ?
              Ok() : BadRequest();
        }


    }
}
