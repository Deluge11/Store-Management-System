using Data_Layer.Data;
using DTOs;

namespace Business_Layer.Business;

public class StocksBusiness 
{
    public StocksData StocksData { get; }
    public AccountsBusiness AccountsBusiness { get; }

    public StocksBusiness(StocksData stocksData, AccountsBusiness accountsBusiness)
    {
        StocksData = stocksData;
        AccountsBusiness = accountsBusiness;
    }

    public async Task<bool> Add(AddNewStock stock)
    {
        if (stock.sellerId < 1 || stock.weight < 0.2m || stock.stockId < 1)
        {
            return false;
        }

        return await StocksData.Add(stock);
    }

    public async Task<bool> AddStockQuantityRequest(InsertProduct stock)
    {
        if (stock.quantity < 1 || stock.receiverId < 1 || stock.stockId < 1 || stock.expiryDate < DateTime.Now.AddDays(3))
        {
            return false;
        }
        return await StocksData.AddStockQuantityRequest(stock);
    }

    public async Task<bool> AcceptAddStockReqeuest(int requestId)
    {
        if(requestId < 1)
        {
            return false;
        }

        int staffId = AccountsBusiness.GetAccountId();

        if(staffId == 0)
        {
            return false;
        }

        return await StocksData.AcceptAddStockReqeuest(requestId, staffId);
    }
}
