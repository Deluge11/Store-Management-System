using Business_Layer.Business;
using Data_Layer.Data;
using DTOs;

namespace Bussiness_Layer.Business
{
    public class TruckBusiness
    {
        public TruckData TruckData { get; }
        public AccountsBusiness AccountsBusiness { get; }

        public TruckBusiness(TruckData truckData, AccountsBusiness accountsBusiness)
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
