using DTOs;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Data
{
    public class InventoryData 
    {
        public string ConnectionString { get; }
        public InventoryData(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public async Task<bool> OpenInventorySession(int staffId)
        {
            using SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            using SqlCommand sqlCommand = new SqlCommand("OpenInventorySession", sqlConnection);

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("@StaffId", SqlDbType.VarChar) { Value = staffId });

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

        public async Task<bool> CloseInventorySession(int staffId)
        {
            using SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            using SqlCommand sqlCommand = new SqlCommand("CloseInventorySession", sqlConnection);

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("@StaffId", SqlDbType.VarChar) { Value = staffId });

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

        public async Task<List<InventoryFirstStageInfo>> GetInventoryFirstStageList(int staffId)
        {
            var result = new List<InventoryFirstStageInfo>();
            using SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            using SqlCommand sqlCommand = new SqlCommand("GetInventoryCheckList", sqlConnection);

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("@StaffId", SqlDbType.Int) { Value = staffId });

            try
            {
                await sqlConnection.OpenAsync();
                using SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    result.Add(new InventoryFirstStageInfo
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("id")),
                        Batch_Number = reader.GetString(reader.GetOrdinal("batch_number")),
                        Expected_Quantity = reader.GetInt32(reader.GetOrdinal("expected_quantity")),
                        Counted_Quantity = reader.IsDBNull(reader.GetOrdinal("counted_quantity")) ?
                          null : reader.GetInt32(reader.GetOrdinal("counted_quantity"))
                    });
                }
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<InventorySecondStageInfo>> GetInventorySecondStageList(int staffId)
        {
            var result = new List<InventorySecondStageInfo>();
            using SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            using SqlCommand sqlCommand = new SqlCommand("GetInventoryDiffenenceList", sqlConnection);

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("@StaffId", SqlDbType.Int) { Value = staffId });

            try
            {
                await sqlConnection.OpenAsync();
                using SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    result.Add(new InventorySecondStageInfo
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("id")),
                        Batch_Number = reader.GetString(reader.GetOrdinal("batch_number")),
                        DifferenceValue = reader.GetInt32(reader.GetOrdinal("difference")),
                    });
                }
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<InventoryDifferenceReasonInfo>> GetDifferenceReasonsList(int staffId, int batchCheckId)
        {
            var result = new List<InventoryDifferenceReasonInfo>();
            using SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            using SqlCommand sqlCommand = new SqlCommand("GetInventoryDiffenenceReasonList", sqlConnection);

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("@StaffId", SqlDbType.Int) { Value = staffId });
            sqlCommand.Parameters.Add(new SqlParameter("@BatchCheckId", SqlDbType.Int) { Value = batchCheckId });

            try
            {
                await sqlConnection.OpenAsync();
                using SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    result.Add(new InventoryDifferenceReasonInfo
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("id")),
                        Quantity = reader.GetInt32(reader.GetOrdinal("quantity")),
                        Reason = reader.GetString(reader.GetOrdinal("reason")),
                    });
                }
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> AddBatchToCheckInventory(int staffId, string batchNumber)
        {
            using SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            using SqlCommand sqlCommand = new SqlCommand("AddBatchToCheckInventory", sqlConnection);

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("@StaffId", SqlDbType.VarChar) { Value = staffId });
            sqlCommand.Parameters.Add(new SqlParameter("@BatchNumber", SqlDbType.VarChar) { Value = batchNumber });

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

        public async Task<bool> UpdateInventoryCountedQuantity(int staffId, int batchCheckId, int quantity)
        {
            using SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            using SqlCommand sqlCommand = new SqlCommand("UpdateInventoryCountedQuantity", sqlConnection);

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("@StaffId", SqlDbType.VarChar) { Value = staffId });
            sqlCommand.Parameters.Add(new SqlParameter("@BatchCheckId", SqlDbType.VarChar) { Value = batchCheckId });
            sqlCommand.Parameters.Add(new SqlParameter("@Quantity", SqlDbType.VarChar) { Value = quantity });

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

        public async Task<bool> AddInventoryDifferenceReason(int staffId, int batchCheckId, int quantity, int reasonId)
        {
            using SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            using SqlCommand sqlCommand = new SqlCommand("AddInventoryDifferenceReason", sqlConnection);

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("@StaffId", SqlDbType.VarChar) { Value = staffId });
            sqlCommand.Parameters.Add(new SqlParameter("@BatchCheckId", SqlDbType.VarChar) { Value = batchCheckId });
            sqlCommand.Parameters.Add(new SqlParameter("@Quantity", SqlDbType.VarChar) { Value = quantity });
            sqlCommand.Parameters.Add(new SqlParameter("@ReasonId", SqlDbType.VarChar) { Value = reasonId });

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

        public async Task<bool> DifferenceReasonStage(int staffId)
        {
            using SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            using SqlCommand sqlCommand = new SqlCommand("InventoryConfirmQuantity", sqlConnection);

            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Parameters.Add(new SqlParameter("@StaffId", SqlDbType.VarChar) { Value = staffId });

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
