using Bussiness_Layer.Interfaces;
using Data_Layer.Interfaces;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness_Layer.Business
{
    public class TruckBusiness : ITruckBusiness
    {
        public ITruckData TruckData { get; }
        public TruckBusiness(ITruckData truckData)
        {
            TruckData = truckData;
        }

        public async Task<List<TruckCatalog>> GetAllConnectedTrucks()
        {
            throw new NotImplementedException();
        }

        public async Task<string> GenerateTruckConnectionString()
        {
            int driverId = 2;
            return await TruckData.GenerateTruckConnectionString(driverId);
        }

        public async Task<bool> ConnectTruck(string truckConnectionString)
        {
            int staffId = 4;
            return await TruckData.ConnectTruck(truckConnectionString, staffId);
        }

        public async Task<bool> LoadingTruck(int truckId, int batchLocationId)
        {
            int staffId = 4;
            return await TruckData.LoadingTruck(truckId, batchLocationId, staffId);
        }

        public async Task<bool> UnLoadingTruck(int truckId, int batchLocationId)
        {
            int staffId = 4;
            return await TruckData.UnLoadingTruck(truckId, batchLocationId,staffId);
        }

        public async Task<bool> TruckDisconnect(int truckId)
        {
            int staffId = 4;
            return await TruckData.TruckDisconnect(truckId, staffId);

        }
    }
}
