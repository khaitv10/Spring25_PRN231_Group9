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
            public DateTime DoseDate { get; set; }

            [Range(1, 10, ErrorMessage = "DoseNumber must be between 1 and 10.")]
            public int? DoseNumber { get; set; }

            [Required(ErrorMessage = "Status is required.")]
            public string? Status { get; set; }

        }
}
