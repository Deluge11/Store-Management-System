using Business_Layer.Interfaces;
using Data_Layer.Interfaces;
using DTOs;

namespace Business_Layer.Business;

public class StocksBusiness : IStocksBusiness
{
    public IStocksData StocksData { get; }
    public IAccountsBusiness AccountsBusiness { get; }

    public StocksBusiness(IStocksData stocksData, IAccountsBusiness accountsBusiness)
    {
        StocksData = stocksData;
        AccountsBusiness = accountsBusiness;
    }

    public async Task<bool> Add(AddNewStock stock)
    {
        return await StocksData.Add(stock);
    }

    public async Task<bool> AddStockQuantityRequest(InsertProduct stock)
    {
        return await StocksData.AddStockQuantityRequest(stock);
    }

    public async Task<bool> AcceptAddStockReqeuest(int requestId)
    {
        int staffId = AccountsBusiness.GetAccountId();
        return await StocksData.AcceptAddStockReqeuest(requestId,staffId);
    }
}
