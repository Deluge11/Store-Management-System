using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public  class InventoryFirstStageInfo
    {
        public int Id { get; set; }
        public string Batch_Number { get; set; }
        public int Expected_Quantity { get; set; }
        public int? Counted_Quantity { get; set; }
    }
}
