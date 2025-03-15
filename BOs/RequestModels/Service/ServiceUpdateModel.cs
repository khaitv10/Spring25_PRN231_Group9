using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BOs.RequestModels.Service
{
    public class ServiceUpdateModel : IValidatableObject
    {
        public string? Name { get; set; } = null!;

        public string? Description { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public decimal? Price { get; set; }

        public bool? Status { get; set; }

        public Dictionary<int, int>? Vaccines { get; set; } = null;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Vaccines != null && Vaccines.Any())
            {
                foreach (var vaccine in Vaccines)
                {
                    if (vaccine.Key <= 0)
                    {
                        yield return new ValidationResult($"Vaccine ID {vaccine.Key} is invalid. It must be a positive number.", new[] { nameof(Vaccines) });
                    }

                    if (vaccine.Value <= 0)
                    {
                        yield return new ValidationResult($"The dose count for vaccine ID {vaccine.Key} must be a positive number.", new[] { nameof(Vaccines) });
                    }
                }
            }
        }
    }
}
