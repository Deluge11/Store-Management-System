using Microsoft.AspNetCore.Mvc;
using DTOs;
using Microsoft.AspNetCore.Authorization;
using Enums;
using Presentation_Layer.Authorization;
using Business_Layer.Business;

namespace Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StocksController : ControllerBase
    {

        public StocksBusiness StocksBusiness { get; }

        public StocksController(StocksBusiness stocksBusiness)
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
