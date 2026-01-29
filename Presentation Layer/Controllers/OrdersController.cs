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
    public class OrdersController : ControllerBase
    {
        public OrdersBusiness OrdersBusiness { get; }

        public OrdersController(OrdersBusiness ordersBusiness)
        {
            OrdersBusiness = ordersBusiness;
        }

        [Authorize(AuthenticationSchemes = "Ecommerce")]
        [HttpPost("{orderId}")]
        public async Task<IActionResult> CreateNewOrder(int orderId, List<OrderRequest> items)
        {
            return await OrdersBusiness.Add(items, orderId) ?
                Ok() : BadRequest("Create New Order Failed");
        }

        [Authorize(AuthenticationSchemes = "Ecommerce")]
        [HttpPost("confrim/{orderId}")]
        public async Task<IActionResult> ConfirmOrder(int orderId)
        {
            return await OrdersBusiness.Confirm(orderId) ?
                Ok() : BadRequest("Something went wrong");
        }

        [Authorize(AuthenticationSchemes = "Ecommerce")]
        [HttpPatch]
        public async Task<IActionResult> SyncOrderItemsCount(List<OrderRequest> items)
        {
            var result = await OrdersBusiness.SyncCount(items);
            return result != null ?
                Ok(result) : BadRequest("Something went wrong");
        }


    }
}
