using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOs.RequestModels.Service
{
    public class ServiceQueryModel
    {
        public string? Name { get; set; }
        public bool? Status { get; set; }
        public decimal FromPrice { get; set; }
        public decimal ToPrice { get; set; }
    }
}
