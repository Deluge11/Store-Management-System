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
            return Ok(await TruckBusiness.GenerateTruckConnectionString());
        }

        [HttpPatch("{truckConnectionString}")]
        public async Task<IActionResult> ConnectToTruck(string truckConnectionString)
        {
            return Ok(await TruckBusiness.ConnectTruck(truckConnectionString));
        }

        [HttpPatch("loading/{truckId}")]
        public async Task<IActionResult> LoadingTruck(int truckId, [FromQuery] int batchLocationId)
        {
            return Ok(await TruckBusiness.LoadingTruck(truckId, batchLocationId));
        }

        [HttpPatch("unloading/{truckId}")]
        public async Task<IActionResult> UnLoadingTruck(int truckId, [FromQuery] int batchLocationId)
        {
            return Ok(await TruckBusiness.UnLoadingTruck(truckId,batchLocationId));

        }

        [HttpPatch("disconnect/{truckId}")]
        public async Task<IActionResult> TruckDisconnect(int truckId)
        {
            return Ok(await TruckBusiness.TruckDisconnect(truckId));

        }

    }
}
