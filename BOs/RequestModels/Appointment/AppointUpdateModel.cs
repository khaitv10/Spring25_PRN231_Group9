using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOs.RequestModels.Appointment
{
    public class AppointUpdateModel
    {
        [Required(ErrorMessage = "Appointment date is required.")]
        public DateTime? AppointmentDate { get; set; }

        [Required(ErrorMessage = "Child selection is required.")]
        public int?  ChildId { get; set; }

        public List<int>? SelectedServiceIds { get; set; } = new List<int>();

        

      

    }
}
