using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOs.RequestModels.Child
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Text.Json.Serialization;

    public class ChildCreateModel
    {
        [Required(ErrorMessage = "Full name is required.")]
        [StringLength(50, ErrorMessage = "Full name cannot be longer than 50 characters.")]
        [RegularExpression(@"^[A-Za-zÀ-ỹ\s]+$", ErrorMessage = "Full name can only contain letters and spaces.")]
        public string FullName { get; set; } = null!;


        [Required(ErrorMessage = "Date of birth is required.")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(ChildCreateModel), nameof(ValidateDob))]
        //[JsonConverter(typeof(DateOnlyJsonConverter))] 
        public DateTime Dob { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        public string Gender { get; set; } = null!;

        [StringLength(200, ErrorMessage = "Note cannot be longer than 200 characters.")]
        public string? Note { get; set; }

        [Required(ErrorMessage = "Parent ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Parent ID must be a valid positive number.")]
        public int ParentId { get; set; }

        public static ValidationResult? ValidateDob(DateTime dob, ValidationContext context)
        {
            if (dob.Date > DateTime.Today)
            {
                return new ValidationResult("Date of birth cannot be in the future.");
            }

            return ValidationResult.Success;
        }

    }

}
