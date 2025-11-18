using Bussiness_Layer.Interfaces;
using Data_Layer.Interfaces;
using DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness_Layer.Business
{
    public class InventoryBusiness : IInventoryBusiness
    {
        public IInventoryData InventoryData { get; }

        public InventoryBusiness(IInventoryData inventoryData)
        {
            InventoryData = inventoryData;
        }


        public async Task<bool> OpenInventorySession()
        {
            int staffId = 5;
            return await InventoryData.OpenInventorySession(staffId);
        }

        public async Task<bool> CloseInventorySession()
        {
            int staffId = 5;
            return await InventoryData.CloseInventorySession(staffId);
        }
        public async Task<List<InventoryFirstStageInfo>> GetInventoryFirstStageList()
        {
            int staffId = 5;
            return await InventoryData.GetInventoryFirstStageList(staffId);
        }

        public async Task<List<InventorySecondStageInfo>> GetInventorySecondStageList()
        {
            int staffId = 5;
            return await InventoryData.GetInventorySecondStageList(staffId);
        }
        public async Task<List<InventoryDifferenceReasonInfo>> GetDifferenceReasonsList(int batchCheckId)
        {
            int staffId = 5;
            return await InventoryData.GetDifferenceReasonsList(staffId, batchCheckId);
        }

        public async Task<bool> AddBatchToCheckInventory(string batchNumber)
        {
            int staffId = 5;
            return await InventoryData.AddBatchToCheckInventory(staffId, batchNumber);
        }

        public async Task<bool> UpdateInventoryCountedQuantity(int batchCheckId, int quantity)
        {
            int staffId = 5;
            return await InventoryData.UpdateInventoryCountedQuantity(staffId, batchCheckId, quantity);
        }

        public async Task<bool> AddInventoryDifferenceReason(int batchCheckId, int quantity, int reasonId)
        {
            int staffId = 5;
            return await InventoryData.AddInventoryDifferenceReason(staffId, batchCheckId, quantity, reasonId);
        }

        public async Task<bool> DifferenceReasonStage()
        {
            int staffId = 5;
            return await InventoryData.DifferenceReasonStage(staffId);
        }

       
    }
}
