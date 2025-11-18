using Microsoft.Data.SqlClient;
using System.Data;
using Data_Layer.Interfaces;
namespace Data_Layer.Data;

public class AuthorizeData : IAuthorizeData
{
    public string ConnectionString { get; }


    public AuthorizeData(string connectionString)
    {
        ConnectionString = connectionString;
    }


    public async Task<List<int>> GetPermissions(int accountId)
    {
        List<int> permissions = new();

        using SqlConnection sqlConnection = new SqlConnection(ConnectionString);
        using SqlCommand sqlCommand = new SqlCommand("GetPermissions", sqlConnection);

        sqlCommand.CommandType = CommandType.StoredProcedure;
        sqlCommand.Parameters.Add(new SqlParameter("@AccountId", SqlDbType.Int) { Value = accountId });

        try
        {
            await sqlConnection.OpenAsync();
            using SqlDataReader reader = await sqlCommand.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                permissions.Add(reader.GetInt32(reader.GetOrdinal("id")));
            }
        }
        catch (Exception)
        {
            return [];
        }

        return permissions;
    }
}
