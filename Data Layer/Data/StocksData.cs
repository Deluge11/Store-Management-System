
using Microsoft.Data.SqlClient;
using System.Data;
using DTOs;
using Azure.Core;

namespace Data_Layer.Data;

public class StocksData 
{

    public string ConnectionString { get; }

    public StocksData(string connectionString)
    {
        ConnectionString = connectionString;
    }


    public async Task<bool> Add(AddNewStock stock)
    {
        string query = @"INSERT INTO Stocks (id,sellerId,weight) VALUES (@id,@sellerId,@weight)";

        using SqlConnection sqlConnection = new SqlConnection(ConnectionString);
        using SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

        sqlCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.Int) { Value = stock.stockId });
        sqlCommand.Parameters.Add(new SqlParameter("@sellerId", SqlDbType.Int) { Value = stock.sellerId });
        sqlCommand.Parameters.Add(new SqlParameter("@weight", SqlDbType.Int) { Value = stock.weight });

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
    public async Task<bool> AddStockQuantityRequest(InsertProduct stock)
    {
    
        using SqlConnection sqlConnection = new SqlConnection(ConnectionString);
        using SqlCommand sqlCommand = new SqlCommand("AddStockQuantityRequest", sqlConnection);

        sqlCommand.CommandType = CommandType.StoredProcedure;
        sqlCommand.Parameters.Add(new SqlParameter("@StockId", SqlDbType.Int) { Value = stock.stockId });
        sqlCommand.Parameters.Add(new SqlParameter("@Quantity", SqlDbType.Int) { Value = stock.quantity });
        sqlCommand.Parameters.Add(new SqlParameter("@ReceiverId", SqlDbType.Int) { Value = stock.receiverId });
        sqlCommand.Parameters.Add(new SqlParameter("@ExpiryDate", SqlDbType.DateTime)
        {
            Value = stock.expiryDate == null ?
            DBNull.Value : stock.expiryDate
        });

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
    public async Task<bool> AcceptAddStockReqeuest(int requestId,int staffId)
    {
        using SqlConnection sqlConnection = new SqlConnection(ConnectionString);
        using SqlCommand sqlCommand = new SqlCommand("AcceptAddStocksRequest", sqlConnection);

        sqlCommand.CommandType = CommandType.StoredProcedure;
        sqlCommand.Parameters.Add(new SqlParameter("@RequestId", SqlDbType.Int) { Value = requestId });
        sqlCommand.Parameters.Add(new SqlParameter("@StaffId", SqlDbType.Int) { Value = staffId });

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
