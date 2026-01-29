
using DTOs;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Data_Layer.Data;

public class OrdersData 
{
    public string ConnectionString { get; }

    public OrdersData(string connectionString)
    {
        ConnectionString = connectionString;
    }


    public async Task<bool> Add(DataTable items, int orderId)
    {
        using SqlConnection sqlConnection = new SqlConnection(ConnectionString);
        using SqlCommand sqlCommand = new SqlCommand("ReserveProductsBulk", sqlConnection);

        var tvpParam = new SqlParameter("@Items", SqlDbType.Structured)
        {
            TypeName = "dbo.OrderItemTable",
            Value = items
        };


        sqlCommand.CommandType = CommandType.StoredProcedure;
        sqlCommand.Parameters.Add(new SqlParameter("@OrderId", SqlDbType.Int) { Value = orderId });
        sqlCommand.Parameters.Add(tvpParam);

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


    public async Task<bool> Confirm(int orderId)
    {
        using SqlConnection sqlConnection = new SqlConnection(ConnectionString);
        using SqlCommand sqlCommand = new SqlCommand("ConfirmOrder", sqlConnection);
        sqlCommand.CommandType = CommandType.StoredProcedure;
        sqlCommand.Parameters.Add(new SqlParameter("@OrderId", SqlDbType.Int) { Value = orderId });

        try
        {
            await sqlConnection.OpenAsync();
            return await sqlCommand.ExecuteNonQueryAsync() > 0;

        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<List<OrderRequest>> SyncCount(DataTable items)
    {
        List<OrderRequest> result = new();

        using SqlConnection sqlConnection = new SqlConnection(ConnectionString);
        using SqlCommand sqlCommand = new SqlCommand("SyncOrderItemsCount", sqlConnection);

        var tvpParam = new SqlParameter("@Items", SqlDbType.Structured)
        {
            TypeName = "dbo.OrderItemTable",
            Value = items
        };

        sqlCommand.CommandType = CommandType.StoredProcedure;
        sqlCommand.Parameters.Add(tvpParam);

        try
        {
            await sqlConnection.OpenAsync();
            using SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                result.Add(new OrderRequest
                {
                    StockId = reader.GetInt32(reader.GetOrdinal("stockId")),
                    Quantity = reader.GetInt32(reader.GetOrdinal("quantity"))
                });
            }
            return result;

        }
        catch (Exception)
        {
            return new List<OrderRequest>();
        }


    }


}
