using Data_Layer.Data;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness_Layer.Interfaces
{
  public  interface IInventoryBusiness
    {
        Task<List<InventoryFirstStageInfo>> GetInventoryFirstStageList();
        Task<List<InventorySecondStageInfo>> GetInventorySecondStageList();
        Task<List<InventoryDifferenceReasonInfo>> GetDifferenceReasonsList(int batchCheckId);
        Task<bool> OpenInventorySession();
        Task<bool> DifferenceReasonStage();
        Task<bool> CloseInventorySession();
        Task<bool> AddBatchToCheckInventory(string batchNumber);
        Task<bool> UpdateInventoryCountedQuantity(int batchCheckId, int quantity);
        Task<bool> AddInventoryDifferenceReason(int batchCheckId, int quantity, int reasonId);
    }
}
