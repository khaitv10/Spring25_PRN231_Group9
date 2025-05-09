﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BOs.RequestModels.Appointment
{
    public class AppointCreateModel : IValidatableObject
    {
        [Required(ErrorMessage = "Appointment date is required.")]
        public DateTime AppointmentDate { get; set; }

        [Required(ErrorMessage = "Child selection is required.")]
        public int ChildId { get; set; }

        public int? ParentId { get; set; }

        public List<int> SelectedServiceIds { get; set; } = new List<int>();

        // Custom Validation
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (AppointmentDate.Date <= DateTime.Today.AddDays(2))
            {
                yield return new ValidationResult("Appointment date must be at least 2 days from today.", new[] { nameof(AppointmentDate) });
            }

            if (SelectedServiceIds == null || SelectedServiceIds.Count == 0)
            {
                yield return new ValidationResult("At least one service must be selected.", new[] { nameof(SelectedServiceIds) });
            }
        }

        [Required(ErrorMessage = "Total price is required.")]
        public decimal? TotalPrice { get; set; }

    }
}
