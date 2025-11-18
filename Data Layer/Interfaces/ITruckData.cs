using DTOs;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Interfaces
{
    public interface ITruckData
    {
        Task<string> GenerateTruckConnectionString(int driverId);
        Task<bool> ConnectTruck(string truckConnectionString, int userId);
        Task<List<TruckCatalog>> GetAllConnectedTrucks();
        Task<bool> LoadingTruck(int truckId, int batchLocationId, int staffId);
        Task<bool> UnLoadingTruck(int truckId, int batchLocationId, int staffId);
        Task<bool> TruckDisconnect(int truckId,int staffId);
    }
}
