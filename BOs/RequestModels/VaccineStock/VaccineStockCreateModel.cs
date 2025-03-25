using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOs.RequestModels.VaccineStock
{
    public class VaccineStockCreateModel
    {
        [Required(ErrorMessage = "Quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative number.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "ExpiryDate is required.")]
        [FutureDate(ErrorMessage = "ExpiryDate must be a future date.")]

        public DateTime ExpiryDate { get; set; }

        [Required(ErrorMessage = "VaccineId is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "VaccineId must be a positive integer.")]
        public int VaccineId { get; set; }
    }
}
