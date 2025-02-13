using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOs.RequestModels.VaccineStock
{
    public class VaccineStockCreateModel
    {
        public int Quantity { get; set; }

        public DateOnly ExpiryDate { get; set; }
        public int VaccineId { get; set; }
    }
}
