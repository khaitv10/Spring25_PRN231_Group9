using BOs.Models; 
using DAO;       
using Microsoft.EntityFrameworkCore; 
using Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{



    public class VaccineRepo : IVaccineRepo 
    {
        private readonly VaccineDAO _vaccineDAO; 
        public VaccineRepo(VaccineDAO vaccineDAO)
        {
            _vaccineDAO = vaccineDAO;
        }

        public async Task<List<Vaccine>> GetAllVaccines()
        {
            return await _vaccineDAO.GetAllVaccines();
        }

        public async Task<Vaccine?> GetVaccineById(int id)
        {
            return await _vaccineDAO.GetVaccineById(id);
        }

        public async Task<Vaccine?> GetVaccineByName(string name)
        {
            return await _vaccineDAO.GetVaccineByName(name);
        }

        public async Task AddVaccine(Vaccine vaccine)
        {
            await _vaccineDAO.AddVaccine(vaccine);
        }

        public async Task UpdateVaccine(Vaccine vaccine)
        {
            await _vaccineDAO.UpdateVaccine(vaccine);
        }

        public async Task DeleteVaccine(int id)
        {
            await _vaccineDAO.DeleteVaccine(id);
        }

        public async Task<bool> IsVaccineNameExists(string name)
        {
            return await _vaccineDAO.IsVaccineNameExists(name);
        }

        public async Task<List<Vaccine>> GetVaccinesByAgeRange(int minAge, int maxAge)
        {
            return await _vaccineDAO.GetVaccinesByAgeRange(minAge, maxAge);
        }


        public async Task<Vaccine?> GetVaccineWithDoseRecords(int id)
        {
            return await _vaccineDAO.GetVaccineWithDoseRecords(id);
        }

        public async Task<List<Vaccine>> GetActiveVaccines()
        {
            return await _vaccineDAO.GetActiveVaccines();
        }
    }
}