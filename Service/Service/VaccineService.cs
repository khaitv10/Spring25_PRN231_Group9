using BOs.Models;
using Repository.IRepositories;
using Repository.Repositories; 
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class VaccineService : IVaccineService 
    {
        private readonly IVaccineRepo _vaccineRepo; 

        public VaccineService(IVaccineRepo vaccineRepo)
        {
            _vaccineRepo = vaccineRepo;
        }

        public async Task<List<Vaccine>> GetAllVaccines()
        {
            return await _vaccineRepo.GetAllVaccines();
        }

        public async Task<Vaccine?> GetVaccineById(int id)
        {
            return await _vaccineRepo.GetVaccineById(id);
        }

        public async Task<Vaccine?> GetVaccineByName(string name)
        {
            return await _vaccineRepo.GetVaccineByName(name);
        }

        public async Task AddVaccine(Vaccine vaccine)
        {
            await _vaccineRepo.AddVaccine(vaccine);
            
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

        public async Task<List<Vaccine>> GetVaccinesByAgeRange(int minAge, int maxAge)
        {
            return await _vaccineRepo.GetVaccinesByAgeRange(minAge, maxAge);
        }

        public async Task<Vaccine?> GetVaccineWithDoseRecords(int id)
        {
            return await _vaccineRepo.GetVaccineWithDoseRecords(id);
        }

        public async Task<List<Vaccine>> GetActiveVaccines()
        {
            return await _vaccineRepo.GetActiveVaccines();
        }


        // Example of business logic in the service:
        public async Task<bool> CanVaccinate(int patientAge, int vaccineId)
        {
            var vaccine = await _vaccineRepo.GetVaccineById(vaccineId);
            if (vaccine == null) return false; // Vaccine doesn't exist

            return patientAge >= vaccine.MinAge && patientAge <= vaccine.MaxAge && vaccine.Status == true; 
        }
    }

   

}