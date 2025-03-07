using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOs.ResponseModels.Vaccine
{
    public class VaccineInfoResponseModel : IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(255, ErrorMessage = "Name cannot exceed 255 characters.")]
        public string Name { get; set; } = null!;

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string? Description { get; set; }

        [StringLength(255, ErrorMessage = "Origin cannot exceed 255 characters.")]
        public string? Origin { get; set; }

        [Range(0, 100, ErrorMessage = "MinAge must be between 0 and 150.")]
        public int? MinAge { get; set; }

        [Range(0, 100, ErrorMessage = "MaxAge must be between 0 and 150.")]
        public int? MaxAge { get; set; }

        public bool Status { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (MinAge.HasValue && MaxAge.HasValue && MinAge > MaxAge)
            {
                yield return new ValidationResult("MinAge must be less than or equal to MaxAge.", new[] { "MinAge", "MaxAge" });
            }

        }
    }
}
