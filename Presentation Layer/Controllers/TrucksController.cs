using Bussiness_Layer.Interfaces;
using Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation_Layer.Authorization;

namespace Presentation_Layer.Controllers
{

    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class TrucksController : ControllerBase
    {

        public ITruckBusiness TruckBusiness { get; }

        public TrucksController(ITruckBusiness truckBusiness)
        {
            TruckBusiness = truckBusiness;
        }


        [CheckPermission(Permission.Trucks_Create_Connection_String)]
        [HttpPost]
        public async Task<IActionResult> GenerageTruckConnectionString()
        {
            string result = await TruckBusiness.GenerateTruckConnectionString();
            return result != null ?
                Ok(result) : BadRequest();
        }


        [CheckPermission(Permission.Trucks_Manage_Connection)]
        [HttpPatch("{truckConnectionString}")]
        public async Task<IActionResult> ConnectToTruck(string truckConnectionString)
        {
            return await TruckBusiness.ConnectTruck(truckConnectionString) ?
                Ok() : BadRequest();
        }


        [CheckPermission(Permission.Trucks_Manage_Connection)]
        [HttpPatch("disconnect/{truckId}")]
        public async Task<IActionResult> TruckDisconnect(int truckId)
        {
            return await TruckBusiness.TruckDisconnect(truckId) ?
                Ok() : BadRequest();
        }


        [CheckPermission(Permission.Trucks_Manage_Stocks)]
        [HttpPatch("loading/{truckId}")]
        public async Task<IActionResult> LoadingTruck(int truckId, [FromQuery] int batchLocationId)
        {
            return await TruckBusiness.LoadingTruck(truckId, batchLocationId) ?
                Ok() : BadRequest();
        }


        [CheckPermission(Permission.Trucks_Manage_Stocks)]
        [HttpPatch("unloading/{truckId}")]
        public async Task<IActionResult> UnLoadingTruck(int truckId, [FromQuery] int batchLocationId)
        {
            return await TruckBusiness.UnLoadingTruck(truckId, batchLocationId) ?
                Ok() : BadRequest();
        }

    }
}
