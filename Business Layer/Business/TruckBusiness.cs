using Business_Layer.Business;
using Business_Layer.Interfaces;
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
        public IAccountsBusiness AccountsBusiness { get; }

        public TruckBusiness(ITruckData truckData, IAccountsBusiness accountsBusiness)
        {
            TruckData = truckData;
            AccountsBusiness = accountsBusiness;
        }

        public async Task<List<TruckCatalog>> GetAllConnectedTrucks()
        {
            throw new NotImplementedException();
        }

        public async Task<string> GenerateTruckConnectionString()
        {
            int driverId = AccountsBusiness.GetAccountId();
            return await TruckData.GenerateTruckConnectionString(driverId);
        }

        public async Task<bool> ConnectTruck(string truckConnectionString)
        {
            int staffId = AccountsBusiness.GetAccountId();
            return await TruckData.ConnectTruck(truckConnectionString, staffId);
        }
        public async Task<bool> TruckDisconnect(int truckId)
        {
            int staffId = AccountsBusiness.GetAccountId();
            return await TruckData.TruckDisconnect(truckId, staffId);
        }

        public async Task<bool> LoadingTruck(int truckId, int batchLocationId)
        {
            int staffId = AccountsBusiness.GetAccountId();
            return await TruckData.LoadingTruck(truckId, batchLocationId, staffId);
        }

        public async Task<bool> UnLoadingTruck(int truckId, int batchLocationId)
        {
            int staffId = AccountsBusiness.GetAccountId();
            return await TruckData.UnLoadingTruck(truckId, batchLocationId,staffId);
        }

       
    }
}
