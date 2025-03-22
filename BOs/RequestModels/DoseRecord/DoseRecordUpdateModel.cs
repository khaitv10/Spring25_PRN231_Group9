using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOs.RequestModels.DoseRecord
{
    public class DoseRecordUpdateModel
    {
        
            [Required(ErrorMessage = "Date of record is required.")]
            [DataType(DataType.Date)]
            [CustomValidation(typeof(DoseRecordCreateModel), nameof(ValidateDoseDate))]
            public DateTime DoseDate { get; set; }

            [Required(ErrorMessage = "DoseNumber is required")]
            [Range(0, 10, ErrorMessage = "DoseNumber must be at least 1")]
            public int? DoseNumber { get; set; }

            [Required(ErrorMessage = "Status is required.")]
            public string? Status { get; set; }
        //public int VaccineId { get; set; }

        //public int ChildId { get; set; }
        public static ValidationResult ValidateDoseDate(DateTime date, ValidationContext context)
            {
                if (date < DateTime.Today)
                {
                    return new ValidationResult("DoseDate must be today or the next day.");
                }
                return ValidationResult.Success;
            }
        }
    }

