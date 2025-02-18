using BOs.Models;
using BOs.RequestModels.Service;
using DAO;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.ServiceRepository
{
    public class ServiceRepsitory : GenericDAO<Service>, IServiceRepository
    {
        public async Task<List<Service>> FilterService(ServiceQueryModel query)
        {
            var predicate = PredicateBuilder.New<Service>(true);

            Expression<Func<Service, bool>> filter = s => true;

            if (!string.IsNullOrEmpty(query.Name))
            {
                filter = filter.And(s => s.Name.Contains(query.Name));
            }

            if (query.Status != null) 
            {
                filter = filter.And(s => s.Status == query.Status);
            }

            if (query.FromPrice > 0)
            {
                filter = filter.And(s => s.Price >= query.FromPrice);
            }

            if (query.ToPrice > 0)
            {
                filter = filter.And(s => s.Price <= query.ToPrice);
            }
            var list = await Get(filter : filter, includeProperties: "ServiceVaccines.Vaccine");
            return list.ToList();
        }

        public async Task<List<Service>> GetActiveServices()
        {
            var list = await Get(includeProperties: "ServiceVaccines.Vaccine",
                                 filter : x => x.Status == true);
            return list.ToList();
        }

        public async Task<List<Service>> GetAllServices()
        {
            var list = await Get(includeProperties: "ServiceVaccines.Vaccine");
            return list.ToList();
        }

        public async Task<Service> GetServiceById(int id)
        {
            return await GetSingle(x => x.Id == id, includeProperties : "ServiceVaccines.Vaccine");
        }

        public async Task<List<Service>> GetServicesByName(string name)
        {
            var list = await Get(includeProperties: "ServiceVaccines.Vaccine",
                                 filter: x => x.Name == name );
            return list.ToList();
        }

        
    }
}
