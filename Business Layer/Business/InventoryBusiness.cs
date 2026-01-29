using Business_Layer.Business;
using Data_Layer.Data;
using DTOs;

namespace Bussiness_Layer.Business
{
    public class InventoryBusiness 
    {
        public InventoryData InventoryData { get; }
        public AccountsBusiness AccountsBusiness { get; }

        public InventoryBusiness(InventoryData inventoryData, AccountsBusiness accountsBusiness)
        {
            InventoryData = inventoryData;
            AccountsBusiness = accountsBusiness;
        }


        public async Task<bool> OpenInventorySession()
        {
            int staffId = AccountsBusiness.GetAccountId();
            return await InventoryData.OpenInventorySession(staffId);
        }

        public async Task<bool> CloseInventorySession()
        {
            int staffId = AccountsBusiness.GetAccountId();
            return await InventoryData.CloseInventorySession(staffId);
        }
        public async Task<List<InventoryFirstStageInfo>> GetInventoryFirstStageList()
        {
            int staffId = AccountsBusiness.GetAccountId();
            return await InventoryData.GetInventoryFirstStageList(staffId);
        }

        public async Task<List<InventorySecondStageInfo>> GetInventorySecondStageList()
        {
            int staffId = AccountsBusiness.GetAccountId();
            return await InventoryData.GetInventorySecondStageList(staffId);
        }
        public async Task<List<InventoryDifferenceReasonInfo>> GetDifferenceReasonsList(int batchCheckId)
        {
            int staffId = AccountsBusiness.GetAccountId();
            return await InventoryData.GetDifferenceReasonsList(staffId, batchCheckId);
        }

        public async Task<bool> AddBatchToCheckInventory(string batchNumber)
        {
            int staffId = AccountsBusiness.GetAccountId();
            return await InventoryData.AddBatchToCheckInventory(staffId, batchNumber);
        }

        public async Task<bool> UpdateInventoryCountedQuantity(int batchCheckId, int quantity)
        {
            int staffId = AccountsBusiness.GetAccountId();
            return await InventoryData.UpdateInventoryCountedQuantity(staffId, batchCheckId, quantity);
        }

        public async Task<bool> AddInventoryDifferenceReason(int batchCheckId, int quantity, int reasonId)
        {
            int staffId = AccountsBusiness.GetAccountId();
            return await InventoryData.AddInventoryDifferenceReason(staffId, batchCheckId, quantity, reasonId);
        }

        public async Task<bool> DifferenceReasonStage()
        {
            int staffId = AccountsBusiness.GetAccountId();
            return await InventoryData.DifferenceReasonStage(staffId);
        }

       
    }
}
