using BOs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOs.ResponseModels.Child
{
    public class ChildDetailResModel
    {
        public int Id { get; set; }

        public string FullName { get; set; } = null!;

        public DateOnly Dob { get; set; }

        public string Gender { get; set; }

        public string Note { get; set; }


        public List<DoseRecordRes> DoseRecords { get; set; } = new();

        public List<DoseSchedulesRes> DoseSchedules { get; set; } = new();

        

    }

    public class DoseRecordRes
    {
        public int Id { get; set; }
        public DateOnly DoseDate { get; set; }
        public int? DoseNumber { get; set; }
        public string VaccineName { get; set; } = null!;
        public string Description { get; set; } = null!;


    }

    public class DoseSchedulesRes
    {
        public int Id { get; set; }
        public DateOnly NextDoseDate { get; set; }
        public int? DoseNumber { get; set; }
        public string VaccineName { get; set; } = null!;
        public string Description { get; set; } = null!;


    }
}
