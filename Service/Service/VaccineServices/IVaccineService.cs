using BOs.Models;
using BOs.RequestModels.Vaccine;
using BOs.ResponseModels.Vaccine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service.VaccineServices
{
    public interface IVaccineService
    {
        Task<List<VaccineInfoResponseModel>> GetAllVaccines();
        Task<VaccineInfoResponseModel> GetVaccineById(int id);
        Task<VaccineInfoResponseModel> GetVaccineByName(string name);
        Task AddVaccine(VaccineCreateModel vaccine);
        Task UpdateVaccine(Vaccine vaccine);
        Task DeleteVaccine(int id);
        Task<bool> IsVaccineNameExists(string name);
        Task<List<VaccineInfoResponseModel>> GetVaccinesByAgeRange(int minAge, int maxAge);
        Task<VaccineInfoResponseModel?> GetVaccineWithDoseRecords(int id);
        Task<List<VaccineInfoResponseModel>> GetActiveVaccines();
        Task<bool> CanVaccinate(int patientAge, int vaccineId);
    }
}

