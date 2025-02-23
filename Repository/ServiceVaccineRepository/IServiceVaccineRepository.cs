using Repository.Repositories.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ServiceVaccineRepository
{
    public interface IServiceVaccineRepository : IGenericRepository<BOs.Models.ServiceVaccine>
    {
         Task<List<BOs.Models.ServiceVaccine>> GetServiceVaccineByServiceId(int serviceId);
    }
}
