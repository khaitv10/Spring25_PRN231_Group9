using BOs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.VaccineRepositories
{
    public interface IVaccineRepository
    {
        Task<List<Vaccine>> GetAllVaccines();
        Task<Vaccine?> GetVaccineById(int id);
        Task<Vaccine?> GetVaccineByName(string name);
        Task AddVaccine(Vaccine vaccine);
        Task UpdateVaccine(Vaccine vaccine);
        Task DeleteVaccine(int id);
        Task<bool> IsVaccineNameExists(string name);
        Task<List<Vaccine>> GetVaccinesByAgeRange(int minAge, int maxAge);
        Task<Vaccine?> GetVaccineWithDoseRecords(int id);
        Task<List<Vaccine>> GetActiveVaccines();
    }
}
