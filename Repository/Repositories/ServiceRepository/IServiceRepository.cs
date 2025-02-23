using BOs.Models;
using BOs.RequestModels.Service;
using Repository.Repositories.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.ServiceRepository
{
    public interface IServiceRepository :IGenericRepository<Service>
    {
        Task<List<Service>> GetAllServices();
        Task<List<Service>> GetActiveServices();
        Task<Service> GetServiceById(int id);
        Task<List<Service>> GetServicesByName(string name);
        Task<List<Service>> FilterService(ServiceQueryModel query);

    }
}
