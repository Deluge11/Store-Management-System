using DTOs;

namespace Business_Layer.Interfaces;

public interface IOrdersBusiness
{
    Task<bool> Add(List<OrderRequest> items, int mappingOrderId);
    Task<bool> Confirm(int orderId);
    Task<List<OrderRequest>> SyncCount(List<OrderRequest> items);
}
