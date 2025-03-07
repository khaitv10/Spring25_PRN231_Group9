using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOs.RequestModels.DoseSchedule
{
    public class DoseScheduleUpdateModel
    {

        [Required(ErrorMessage = "NextDoseDate is required.")]
        [DataType(DataType.Date)]
        [FutureDate(ErrorMessage = "Next Dose Date must be in the future.")]
        public DateTime NextDoseDate { get; set; }
        [Required(ErrorMessage = "DoseNumber is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "DoseNumber must be at least 1.")]
        public int? DoseNumber { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [StringLength(50, ErrorMessage = "Status cannot exceed 50 characters.")]
        public string? Status { get; set; }
        public int VaccineId { get; set; }

        public int ChildId { get; set; }

        public class FutureDateAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value is DateTime date)
                {
                    if (date.Date <= DateTime.Today)
                    {
                        return new ValidationResult("Next Dose Date must be a future date.");
                    }
                }
                return ValidationResult.Success;
            }
        }

    }
}
