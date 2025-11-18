using Business_Layer.Interfaces;
using Data_Layer.Interfaces;
using System.Data;
using DTOs;

namespace Business_Layer.Business;

public class OrdersBusiness : IOrdersBusiness
{
    public IOrdersData OrdersData { get; }

    public OrdersBusiness(IOrdersData ordersData)
    {
        OrdersData = ordersData;
    }


    public async Task<bool> Add(List<OrderRequest> items, int orderId)
    {
        return await OrdersData.Add(ToDataTable(items), orderId);
    }

    public async Task<bool> Confirm(int orderId)
    {
        return await OrdersData.Confirm(orderId);
    }
    public async Task<List<OrderRequest>> SyncCount(List<OrderRequest> items)
    {
        return await OrdersData.SyncCount(ToDataTable(items));
    }

    private DataTable ToDataTable(List<OrderRequest> items)
    {
        var table = new DataTable();
        table.Columns.Add("stock_id", typeof(int));
        table.Columns.Add("quantity", typeof(int));

        foreach (var item in items)
        {
            table.Rows.Add(item.StockId, item.Quantity);
        }

        return table;
    }
}
