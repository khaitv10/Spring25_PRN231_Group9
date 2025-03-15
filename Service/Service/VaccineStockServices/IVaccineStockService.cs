using BOs.Models;
using BOs.RequestModels.VaccineStock;
using BOs.ResponseModels.VaccineStock;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Service.VaccineStockServices
{
    public interface IVaccineStockService
    {
        Task<List<VaccineStockResponseModel>> GetAllVaccineStocks();
        Task<VaccineStockResponseModel> GetVaccineStockById(int id);
        Task<List<VaccineStockResponseModel>> GetVaccineStocksByVaccineId(int vaccineId);
        Task AddVaccineStock(VaccineStockCreateModel create);
        Task UpdateVaccineStock(int id, VaccineStockUpdateModel update);
        Task DeleteVaccineStock(int id);
    }
}