using BOs.Models;
using BOs.ResponseModels.Child;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOs.ResponseModels.Appointment
{
    public class AppointmentResModel
    {
        public int Id { get; set; }

        public DateTime AppointmentDate { get; set; }

        public string? Type { get; set; }

        public decimal? TotalPrice { get; set; }

        public string? Status { get; set; }

        public string? PaymentStatus { get; set; }

        public ChildResponse Child { get; set; } = new();
        public List<ServiceResponse> Services{ get; set; } = new();


    }

    public class ChildResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string? Gender { get; set; }

    }

    public class ServiceResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? TotalDoses { get; set; }
    }

}
