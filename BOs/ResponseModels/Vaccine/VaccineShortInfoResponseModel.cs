using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOs.ResponseModels.Vaccine
{
    public class VaccineShortInfoResponseModel
    {
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Description { get; set; }
    }
}
