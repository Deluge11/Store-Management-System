using Bussiness_Layer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation_Layer.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class TrucksController : ControllerBase
    {

        public ITruckBusiness TruckBusiness { get; }

        public TrucksController(ITruckBusiness truckBusiness)
        {
            TruckBusiness = truckBusiness;
        }


        [HttpPost]
        public async Task<IActionResult> GenerageTruckConnectionString()
        {
            string result = await TruckBusiness.GenerateTruckConnectionString();
            return result != null ?
                Ok(result) : BadRequest();
        }

        [HttpPatch("{truckConnectionString}")]
        public async Task<IActionResult> ConnectToTruck(string truckConnectionString)
        {
            return await TruckBusiness.ConnectTruck(truckConnectionString) ?
                Ok() : BadRequest();
        }

        [HttpPatch("loading/{truckId}")]
        public async Task<IActionResult> LoadingTruck(int truckId, [FromQuery] int batchLocationId)
        {
            return await TruckBusiness.LoadingTruck(truckId, batchLocationId) ?
                Ok() : BadRequest();
        }

        [HttpPatch("unloading/{truckId}")]
        public async Task<IActionResult> UnLoadingTruck(int truckId, [FromQuery] int batchLocationId)
        {
            return await TruckBusiness.UnLoadingTruck(truckId, batchLocationId) ?
                Ok() : BadRequest();

        }

        [HttpPatch("disconnect/{truckId}")]
        public async Task<IActionResult> TruckDisconnect(int truckId)
        {
            return await TruckBusiness.TruckDisconnect(truckId) ?
                Ok() : BadRequest();

        }

    }
}
