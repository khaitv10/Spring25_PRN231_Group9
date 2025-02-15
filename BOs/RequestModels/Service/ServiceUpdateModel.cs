using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOs.RequestModels.Service
{
    public class ServiceUpdateModel
    {
        public string? Name { get; set; } = null!;

        public string? Description { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public decimal? Price { get; set; }

        public bool? Status { get; set; }
        public Dictionary<int, int>? Vaccines { get; set; } = null;
    }
}
