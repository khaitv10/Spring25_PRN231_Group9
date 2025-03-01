using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOs.RequestModels.DoseSchedule
{
    public class DoseScheduleUpdateModel
    {
        public int Id { get; set; }
        public DateOnly NextDoseDate { get; set; }

        public int? DoseNumber { get; set; }

        public string? Status { get; set; }

        
    }
}
