using DTOs;

namespace Business_Layer.Interfaces;

public interface IStocksBusiness
{
    Task<bool> Add(AddNewStock stock);
    Task<bool> AddStockQuantityRequest(InsertProduct stock);
    Task<bool> AcceptAddStockReqeuest(int requestId);
}
