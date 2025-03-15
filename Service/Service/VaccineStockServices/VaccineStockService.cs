using AutoMapper;
using BOs.Models; 
using BOs.RequestModels.VaccineStock;
using BOs.ResponseModels.VaccineStock;
using Repository.Repositories.VaccineRepositories;
using Repository.Repositories.VaccineStockRepositories; 
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.Service.VaccineStockServices
{
    public class VaccineStockService : IVaccineStockService
    {
        private readonly IVaccineStockRepository _vaccineStockRepository;
        private readonly IVaccineRepository _vaccineRepository;
        private readonly IMapper _mapper;

        public VaccineStockService(IVaccineStockRepository vaccineStockRepository, IMapper mapper, IVaccineRepository vaccineRepository) 
        {
            _vaccineStockRepository = vaccineStockRepository;
            _mapper = mapper;
            _vaccineRepository = vaccineRepository;
        }

        public async Task<List<VaccineStockResponseModel>> GetAllVaccineStocks()
        {
            var list = await _vaccineStockRepository.GetAllVaccineStocks();
            foreach(VaccineStock s in list)
            {
                s.Vaccine = await _vaccineRepository.GetVaccineById(s.VaccineId);
            }
            return _mapper.Map<List<VaccineStockResponseModel>>(list);   
        }

        public async Task<VaccineStockResponseModel> GetVaccineStockById(int id)
        {
            var vaccineStock = await _vaccineStockRepository.GetVaccineStockById(id);
            vaccineStock.Vaccine = await _vaccineRepository.GetVaccineById(vaccineStock.VaccineId);
            return _mapper.Map<VaccineStockResponseModel>(vaccineStock);
        }

        public async Task<List<VaccineStockResponseModel>> GetVaccineStocksByVaccineId(int vaccineId)
        {
            var list = await _vaccineStockRepository.GetVaccineStocksByVaccineId(vaccineId);
            return _mapper.Map<List<VaccineStockResponseModel>>(list);
        }

        public async Task AddVaccineStock(VaccineStockCreateModel create)
        {
            VaccineStock newVaccineStock = _mapper.Map<VaccineStock>(create);
            await _vaccineStockRepository.AddVaccineStock(newVaccineStock);
        }

        public async Task UpdateVaccineStock(int id, VaccineStockUpdateModel update)
        {
            var vaccineStock = await _vaccineStockRepository.GetVaccineStockById(id);
            if (vaccineStock != null)
            {
                vaccineStock.Quantity = update.Quantity;
                vaccineStock.ExpiryDate = new DateOnly(update.ExpiryDate.Year, update.ExpiryDate.Month, update.ExpiryDate.Day);
                await _vaccineStockRepository.UpdateVaccineStock(vaccineStock);
            }
            }

        public async Task DeleteVaccineStock(int id)
        {
            await _vaccineStockRepository.DeleteVaccineStock(id);
        }


    }
}