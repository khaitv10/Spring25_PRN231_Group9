using BOs.ResponseModels.Vaccine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOs.ResponseModels.VaccineStock
{
    public class VaccineStockResponseModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }

        public DateOnly ExpiryDate { get; set; }

        public DateTime? UpdateAt { get; set; }
        public VaccineShortInfoResponseModel Vaccine { get; set; } = null!;
    }
}
