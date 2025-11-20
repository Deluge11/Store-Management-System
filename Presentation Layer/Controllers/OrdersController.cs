using Microsoft.AspNetCore.Mvc;
using Business_Layer.Interfaces;
using DTOs;
using Microsoft.AspNetCore.Authorization;
using Enums;
using Presentation_Layer.Authorization;

namespace Controllers
{
    [Authorize]
    [ApiController]
    [CheckPermission(Permission.Orders_Manage)]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        public IOrdersBusiness OrdersBusiness { get; }

        public OrdersController(IOrdersBusiness ordersBusiness)
        {
            OrdersBusiness = ordersBusiness;
        }

        
        [HttpPost("{orderId}")]
        public async Task<IActionResult> CreateNewOrder(int orderId , List<OrderRequest> items)
        {
            return await OrdersBusiness.Add(items, orderId) ?
                Ok() : BadRequest("Create New Order Failed");
        }

        
        [HttpPost("confrim/{orderId}")]
        public async Task<IActionResult> ConfirmOrder(int orderId)
        {
            return await OrdersBusiness.Confirm(orderId) ?
                Ok() : BadRequest("Something went wrong");
        }

        
        [HttpPatch]
        public async Task<IActionResult> SyncOrderItemsCount(List<OrderRequest> items)
        {
            var result = await OrdersBusiness.SyncCount(items);
            return result != null ?
                Ok(result) : BadRequest("Something went wrong");
        }


    }
}
