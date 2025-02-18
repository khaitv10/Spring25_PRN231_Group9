using BOs.ResponseModels.Vaccine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOs.ResponseModels.Service
{
    public class ServiceVaccineResponseModel
    {
        public int? NumberOfDose { get; set; }
        public VaccineInfoResponseModel VaccineInfo { get; set; }
    }
}
