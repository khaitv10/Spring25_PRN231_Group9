using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOs.RequestModels.DoseSchedule
{
    public class DoseScheduleCreateModel
    {
        [Required(ErrorMessage = "NextDoseDate is required.")]
        [DataType(DataType.Date)]
        public DateTime NextDoseDate { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "DoseNumber must be at least 1.")]
        public int? DoseNumber { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [StringLength(50, ErrorMessage = "Status cannot exceed 50 characters.")]
        public string? Status { get; set; }


    }
}
