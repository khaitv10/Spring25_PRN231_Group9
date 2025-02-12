using AutoMapper;
using BOs.Models;
using BOs.RequestModels.Vaccine;
using BOs.ResponseModels.Child;
using BOs.ResponseModels.Vaccine;
using Repository.Repositories;
using Repository.Repositories.VaccineRepositories;
using Service.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service.VaccineServices
{
    public class VaccineService : IVaccineService
    {
        private readonly IVaccineRepository _vaccineRepo;
        private readonly IMapper _mapper;

        public VaccineService(IVaccineRepository vaccineRepo, IMapper mapper)
        {
            _vaccineRepo = vaccineRepo;
            _mapper = mapper;
        }

        public async Task<List<VaccineInfoResponseModel>> GetAllVaccines()
        {
            var list = await _vaccineRepo.GetAllVaccines();
            return _mapper.Map<List<VaccineInfoResponseModel>>(list);
     
        }

        public async Task<VaccineInfoResponseModel> GetVaccineById(int id)
        {
            var vaccine = await _vaccineRepo.GetVaccineById(id);
            return _mapper.Map<VaccineInfoResponseModel>(vaccine);
        }

        public async Task<VaccineInfoResponseModel> GetVaccineByName(string name)
        {
            var vaccine = await _vaccineRepo.GetVaccineByName(name);
            return _mapper.Map<VaccineInfoResponseModel>(vaccine);
        }

        public async Task AddVaccine(VaccineCreateModel vaccine)
        {
            Vaccine newVaccine = _mapper.Map<Vaccine>(vaccine);
            await _vaccineRepo.AddVaccine(newVaccine);

        }

        public async Task UpdateVaccine(Vaccine vaccine)
        {
            await _vaccineRepo.UpdateVaccine(vaccine);

        }

        public async Task DeleteVaccine(int id)
        {
            await _vaccineRepo.DeleteVaccine(id);

        }

        public async Task<bool> IsVaccineNameExists(string name)
        {
            return await _vaccineRepo.IsVaccineNameExists(name);
        }

        public async Task<List<VaccineInfoResponseModel>> GetVaccinesByAgeRange(int minAge, int maxAge)
        {
            var list = await _vaccineRepo.GetVaccinesByAgeRange(minAge, maxAge);
            return _mapper.Map<List<VaccineInfoResponseModel>>(list);
        }

        public async Task<VaccineInfoResponseModel?> GetVaccineWithDoseRecords(int id)
        {
            var vaccine =  await _vaccineRepo.GetVaccineWithDoseRecords(id);
            return _mapper.Map<VaccineInfoResponseModel>(vaccine);
        }

        public async Task<List<VaccineInfoResponseModel>> GetActiveVaccines()
        {
            var list = await _vaccineRepo.GetActiveVaccines();
            return _mapper.Map<List<VaccineInfoResponseModel>>(list);
        }


  
        public async Task<bool> CanVaccinate(int patientAge, int vaccineId)
        {
            var vaccine = await _vaccineRepo.GetVaccineById(vaccineId);
            if (vaccine == null) return false; 

            return patientAge >= vaccine.MinAge && patientAge <= vaccine.MaxAge && vaccine.Status == true;
        }
    }



}