﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOs.ResponseModels.Vaccine
{
    public class VaccineInfoResponseModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public string? Origin { get; set; }

        public int? MinAge { get; set; }

        public int? MaxAge { get; set; }

        public bool? Status { get; set; }
    }
}
