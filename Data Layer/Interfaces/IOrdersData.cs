using DTOs;
using System.Data;

namespace Data_Layer.Interfaces;

public interface IOrdersData
{
    Task<bool> Add(DataTable items,int orderId);
    Task<bool> Confirm(int orderId);
    Task<List<OrderRequest>> SyncCount(DataTable items);
}
