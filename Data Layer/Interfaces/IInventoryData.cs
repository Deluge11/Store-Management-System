using DTOs;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Interfaces
{
    public interface IInventoryData
    {
        Task<List<InventoryFirstStageInfo>> GetInventoryFirstStageList(int staffId);
        Task<List<InventorySecondStageInfo>> GetInventorySecondStageList(int staffId);
        Task<List<InventoryDifferenceReasonInfo>> GetDifferenceReasonsList(int staffId, int batchCheckId);
        Task<bool> OpenInventorySession(int staffId);
        Task<bool> DifferenceReasonStage(int staffId);
        Task<bool> CloseInventorySession(int staffId);
        Task<bool> AddBatchToCheckInventory(int staffId, string batchNumber);
        Task<bool> UpdateInventoryCountedQuantity(int staffId, int batchCheckId, int quantity);
        Task<bool> AddInventoryDifferenceReason(int staffId, int batchCheckId, int quantity, int reasonId);

    }
}
