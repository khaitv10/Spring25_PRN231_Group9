using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOs.RequestModels.Payment
{
    public class PaymentCallbackRequest
    {
        public int AppointmentId { get; set; }
        public long OrderCode { get; set; }
    }

}
