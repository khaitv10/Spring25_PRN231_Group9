using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOs.RequestModels.DoseRecord
{
    public class DoseRecordUpdateModel
    {
        public DateOnly DoseDate { get; set; }

        public int? DoseNumber { get; set; }

        public string? Status { get; set; }

       
    }
}
