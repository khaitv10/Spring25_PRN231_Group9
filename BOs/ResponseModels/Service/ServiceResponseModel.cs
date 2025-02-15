using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOs.ResponseModels.Service
{
    public class ServiceResponseModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public decimal? Price { get; set; }

        public int? TotalDoses { get; set; }

        public DateTime? CreateAt { get; set; }

        public DateTime? UpdateAt { get; set; }

        public bool? Status { get; set; }
        public List<ServiceVaccineResponseModel> Vaccine { get; set; }
    }
}
