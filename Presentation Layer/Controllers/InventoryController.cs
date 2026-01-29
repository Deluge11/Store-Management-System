using Bussiness_Layer.Business;
using Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation_Layer.Authorization;

namespace Presentation_Layer.Controllers
{
    [Authorize]
    [ApiController]
    [CheckPermission(Permission.Inventory_Check)]
    [Route("[controller]")]
    public class InventoryController : ControllerBase
    {
        public InventoryBusiness InventoryBusiness { get; }

        public InventoryController(InventoryBusiness inventoryBusiness)
        {
            InventoryBusiness = inventoryBusiness;
        }


        
        [HttpPost("start-session")]
        public async Task<IActionResult> StartInventroySession()
        {
            return await InventoryBusiness.OpenInventorySession() ?
                 Ok() : BadRequest();
        }



        [HttpPatch("close-session")]
        public async Task<IActionResult> CloseInventroySession()
        {
            return await InventoryBusiness.CloseInventorySession() ?
                 Ok() : BadRequest();

        }



        [HttpGet("first-stage-list")]
        public async Task<IActionResult> GetInventoryFirstStageList()
        {
            var result = await InventoryBusiness.GetInventoryFirstStageList();
            return result != null ?
                Ok(result) : NotFound();
        }



        [HttpGet("second-stage-list")]
        public async Task<IActionResult> GetInventorySecondStageList()
        {
            var result = await InventoryBusiness.GetInventorySecondStageList();
            return result != null ?
                Ok(result) : NotFound();
        }



        [HttpGet("difference-reasons/{batchCheckId}")]
        public async Task<IActionResult> GetDifferenceReasons(int batchCheckId)
        {
            var result = await InventoryBusiness.GetDifferenceReasonsList(batchCheckId);
            return result != null ?
                Ok(result) : NotFound();
        }



        [HttpPost("add-batch-to-inventory/{batchNumber}")]
        public async Task<IActionResult> AddBatchToCheckInventory(string batchNumber)
        {
            return await InventoryBusiness.AddBatchToCheckInventory(batchNumber) ?
                 Ok() : BadRequest();
        }



        [HttpPatch("count-batch-quantity/{batchCheckId}")]
        public async Task<IActionResult> UpdateInventoryCountedQuantity(int batchCheckId, [FromQuery] int countedQuantity)
        {
            return await InventoryBusiness.UpdateInventoryCountedQuantity(batchCheckId, countedQuantity) ?
                 Ok() : BadRequest();
        }



        [HttpPatch("diff-reason-stage")]
        public async Task<IActionResult> DifferenceReasonStage()
        {
            return await InventoryBusiness.DifferenceReasonStage() ?
                 Ok() : BadRequest();
        }



        [HttpPost("add-diff-reason/{batchCheckId}")]
        public async Task<IActionResult> AddInventoryDifferenceReason(int batchCheckId, [FromQuery] int quantity, [FromQuery] int reasonId)
        {
            return await InventoryBusiness.AddInventoryDifferenceReason(batchCheckId, quantity, reasonId) ?
                 Ok() : BadRequest();
        }

    }
}
