using BOs.Models;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.VaccineStockRepositories
{
    public class VaccineStockRepository : GenericDAO<VaccineStock>, IVaccineStockRepository
    {


        public async Task<List<VaccineStock>> GetAllVaccineStocks()
        {
            var list =  await Get();
            return list.ToList();
        }

        public async Task<VaccineStock> GetVaccineStockById(int id)
        {
            return await GetByID(id);
        }

        public async Task<List<VaccineStock>> GetVaccineStocksByVaccineId(int vaccineId)
        {
            var list =  await Get(x=>x.VaccineId == vaccineId);
            return list.ToList();
        }

        public async Task AddVaccineStock(VaccineStock vaccineStock)
        {
            await Insert(vaccineStock);
        }

        public async Task UpdateVaccineStock(VaccineStock vaccineStock)
        {
            await Update(vaccineStock);
        }

        public async Task DeleteVaccineStock(int id)
        {
            var vaccineStock = await GetVaccineStockById(id);
            if (vaccineStock != null)
            {
                await Delete(vaccineStock);
            }
        }


    }
}
