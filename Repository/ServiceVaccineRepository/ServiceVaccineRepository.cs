using DAO;
using Repository.ServiceVaccineRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ServiceVaccine
{
    public class ServiceVaccineRepository : GenericDAO<BOs.Models.ServiceVaccine>, IServiceVaccineRepository
    {
        public async Task<List<BOs.Models.ServiceVaccine>> GetServiceVaccineByServiceId(int serviceId)
        {
            var list = await Get(x => x.ServiceId == serviceId);
            return list.ToList();
        }
    }
}
