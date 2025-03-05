using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BOs.RequestModels.Service
{
    public class ServiceCreateModel 
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public decimal? Price { get; set; }

        public bool? Status { get; set; }

        [Required(ErrorMessage = "At least one vaccine must be selected.")]
        public Dictionary<int, int> Vaccines { get; set; } = new Dictionary<int, int>();

    }
}
