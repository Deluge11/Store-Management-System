using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness_Layer.Interfaces
{
   public interface ITruckBusiness
    {
        Task<List<TruckCatalog>> GetAllConnectedTrucks();
        Task<string> GenerateTruckConnectionString();
        Task<bool> ConnectTruck(string truckConnectionString);
        Task<bool> LoadingTruck(int truckId,int batchLocationId);
        Task<bool> UnLoadingTruck(int truckId, int batchLocationId);
        Task<bool> TruckDisconnect(int truckId);
    }
}
