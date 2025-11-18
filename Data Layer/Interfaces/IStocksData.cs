using DTOs;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Data_Layer.Interfaces;

public interface IStocksData
{
     Task<bool> Add(AddNewStock stock);
     Task<bool> AddStockQuantityRequest(InsertProduct stock);
     Task<bool> AcceptAddStockReqeuest(int requestId, int staffId);
}
