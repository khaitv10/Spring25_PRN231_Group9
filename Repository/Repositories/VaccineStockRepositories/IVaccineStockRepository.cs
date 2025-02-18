using BOs.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Repositories.VaccineStockRepositories
{
    public interface IVaccineStockRepository
    {
        Task<List<VaccineStock>> GetAllVaccineStocks();
        Task<VaccineStock> GetVaccineStockById(int id);
        Task<List<VaccineStock>> GetVaccineStocksByVaccineId(int vaccineId);
        Task AddVaccineStock(VaccineStock vaccineStock);
        Task UpdateVaccineStock(VaccineStock vaccineStock);
        Task DeleteVaccineStock(int id);
    }
}