using System.Data;
using Data_Layer.Data;
using DTOs;

namespace Business_Layer.Business;

public class OrdersBusiness 
{
    public OrdersData OrdersData { get; }

    public OrdersBusiness(OrdersData ordersData)
    {
        OrdersData = ordersData;
    }


    public async Task<bool> Add(List<OrderRequest> items, int orderId)
    {
        if(items == null || items.Count < 1 || orderId < 1)
        {
            return false;
        }
        return await OrdersData.Add(ToDataTable(items), orderId);
    }

    public async Task<bool> Confirm(int orderId)
    {
        if (orderId < 1)
        {
            return false;
        }
        return await OrdersData.Confirm(orderId);
    }
    public async Task<List<OrderRequest>> SyncCount(List<OrderRequest> items)
    {
        if (items == null || items.Count < 1)
        {
            return null;
        }

        var datatable = ToDataTable(items);

        if (datatable == null || datatable.Rows.Count < 1)
        {
            return null;
        }

        return await OrdersData.SyncCount(datatable);
    }

    private DataTable ToDataTable(List<OrderRequest> items)
    {
        if (items == null || items.Count < 1)
        {
            return null;
        }

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
