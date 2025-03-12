using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOs.RequestModels.VaccineStock
{
    public class VaccineStockUpdateModel
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public DateTime ExpiryDate { get; set; }

        public DateTime? UpdateAt { get; set; } = DateTime.Now;
    }
}
