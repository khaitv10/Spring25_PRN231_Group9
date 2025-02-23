using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOs.ResponseModels.DoseRecord
{
    public class DoseRecordResponseModel
    {
        public int Id { get; set; }

        public DateOnly DoseDate { get; set; }

        public int? DoseNumber { get; set; }

        public string? Status { get; set; }

        public DateTime? CreateAt { get; set; }
        public string VaccineName { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
