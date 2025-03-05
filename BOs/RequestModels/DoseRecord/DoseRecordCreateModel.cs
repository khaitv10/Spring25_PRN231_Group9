using BOs.RequestModels.Child;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOs.RequestModels.DoseRecord
{
    public class DoseRecordCreateModel
    {
    [Required(ErrorMessage = "Date of record is required.")]
    [DataType(DataType.Date)]
    [CustomValidation(typeof(DoseRecordCreateModel), nameof(ValidateDoseDate))]
    public DateTime DoseDate { get; set; }

    [Required(ErrorMessage = "DoseNumber is required")]
    [Range(1, 10, ErrorMessage = "DoseNumber must be between 1 and 10.")]
    public int? DoseNumber { get; set; }


    [Required(ErrorMessage = "Status is required.")]
    public string? Status { get; set; }



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
