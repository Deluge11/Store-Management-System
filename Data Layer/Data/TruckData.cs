using Data_Layer.Interfaces;
using DTOs;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Data_Layer.Data
{
    public class TruckData : ITruckData
    {
        public string ConnectionString { get; }

        public TruckData(string connectionString)
        {
            ConnectionString = connectionString;
        }
        public async Task<List<TruckCatalog>> GetAllConnectedTrucks()
        {
            List<TruckCatalog> result = new();
            using SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            using SqlCommand sqlCommand = new SqlCommand("GetAllConnectedTrucks", sqlConnection);

            sqlCommand.CommandType = CommandType.StoredProcedure;
            try
            {
                await sqlConnection.OpenAsync();
                using SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    result.Add(new TruckCatalog
                    {
                        id = reader.GetInt32(reader.GetOrdinal("id")),
                        number = reader.GetString(reader.GetOrdinal("number"))
                    });
                }
                return result;
            }
            catch (Exception)
            {
                return null;
            }

        }
        public async Task<string> GenerateTruckConnectionString(int driverId)
        {
            using SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            using SqlCommand sqlCommand = new SqlCommand("GenerateTruckConnectionString", sqlConnection);

            sqlCommand.Parameters.Add(new SqlParameter("@DriverId", SqlDbType.Int) { Value = driverId });
            sqlCommand.CommandType = CommandType.StoredProcedure;
            try
            {
                await sqlConnection.OpenAsync();
                using SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    return reader.GetString(reader.GetOrdinal("connectionstring"));
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
        public async Task<bool> ConnectTruck(string truckConnectionString,int userId)
        {
            if (string.IsNullOrEmpty(truckConnectionString))
            {
                return false;
            }

            using SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            using SqlCommand sqlCommand = new SqlCommand("UseTruckConnectionString", sqlConnection);

            sqlCommand.Parameters.Add(new SqlParameter("@ConnectionString", SqlDbType.VarChar) { Value = truckConnectionString });
            sqlCommand.Parameters.Add(new SqlParameter("@StaffId", SqlDbType.VarChar) { Value = userId });
            sqlCommand.CommandType = CommandType.StoredProcedure;
            try
            {
                await sqlConnection.OpenAsync();
                await sqlCommand.ExecuteNonQueryAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public async Task<bool> LoadingTruck(int truckId, int batchLocationId,int staffId)
        {
            using SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            using SqlCommand sqlCommand = new SqlCommand("LoadingTruck", sqlConnection);

            sqlCommand.Parameters.Add(new SqlParameter("@BatchLocationId", SqlDbType.Int) { Value = batchLocationId });
            sqlCommand.Parameters.Add(new SqlParameter("@TruckId", SqlDbType.Int) { Value = truckId });
            sqlCommand.Parameters.Add(new SqlParameter("@StaffId", SqlDbType.Int) { Value = staffId });
            sqlCommand.CommandType = CommandType.StoredProcedure;
            try
            {
                await sqlConnection.OpenAsync();
                await sqlCommand.ExecuteNonQueryAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> UnLoadingTruck(int truckId, int batchLocationId,int staffId)
        {
            using SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            using SqlCommand sqlCommand = new SqlCommand("UnLoadingTruck", sqlConnection);

            sqlCommand.Parameters.Add(new SqlParameter("@BatchLocationId", SqlDbType.Int) { Value = batchLocationId });
            sqlCommand.Parameters.Add(new SqlParameter("@TruckId", SqlDbType.Int) { Value = truckId });
            sqlCommand.Parameters.Add(new SqlParameter("@StaffId", SqlDbType.Int) { Value = staffId });
            sqlCommand.CommandType = CommandType.StoredProcedure;
            try
            {
                await sqlConnection.OpenAsync();
                await sqlCommand.ExecuteNonQueryAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<bool> TruckDisconnect(int truckId, int staffId)
        {

            using SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            using SqlCommand sqlCommand = new SqlCommand("TruckDisconnect", sqlConnection);

            sqlCommand.Parameters.Add(new SqlParameter("@TruckId", SqlDbType.Int) { Value = truckId });
            sqlCommand.Parameters.Add(new SqlParameter("@StaffId", SqlDbType.Int) { Value = staffId });
            sqlCommand.CommandType = CommandType.StoredProcedure;
            try
            {
                await sqlConnection.OpenAsync();
                await sqlCommand.ExecuteNonQueryAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
