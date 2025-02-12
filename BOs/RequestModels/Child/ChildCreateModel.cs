﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOs.RequestModels.Child
{
    public class ChildCreateModel
    {
        [StringLength(50, ErrorMessage = "Full name cannot be longer than 50 characters.")]
        [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "Name can only contain letters and spacssses.")]
        public string FullName { get; set; } = null!;

        public DateOnly Dob { get; set; }

        public string? Gender { get; set; }

        public string? Note { get; set; }
    }
}
