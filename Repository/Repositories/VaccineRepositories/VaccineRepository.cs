using BOs.Models;
using DAO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.VaccineRepositories
{



    public class VaccineRepository : GenericDAO<Vaccine>, IVaccineRepository
    {

        public async Task<List<Vaccine>> GetAllVaccines()
        {
            var list = await Get();
            return list.ToList();
        }

        public async Task<Vaccine?> GetVaccineById(int id)
        {
            return await GetSingle(x => x.Id == id);
        }

        public async Task<Vaccine?> GetVaccineByName(string name)
        {
            return await GetSingle(x => x.Name == name);
            
        }

        public async Task AddVaccine(Vaccine vaccine)
        {
            await Insert(vaccine);
        }

        public async Task UpdateVaccine(Vaccine vaccine)
        {
            await Update(vaccine); 
        }

        public async Task DeleteVaccine(int id)
        {
            var vaccine = await GetVaccineById(id);
            if(vaccine != null)
            {
                vaccine.Status = false;
                await Update(vaccine);
            }       
        }

        public async Task<bool> IsVaccineNameExists(string name)
        {
            return await Get(x=>x.Name == name) != null ? true : false;
        }

        public async Task<List<Vaccine>> GetVaccinesByAgeRange(int minAge, int maxAge)
        {
            var list = await Get(x=>x.MinAge >= minAge && x.MaxAge <= maxAge);
            return list.ToList();
        }

        public async Task<List<Vaccine>> GetActiveVaccines()
        {
            var list = await Get(x => x.Status == true);
            return list.ToList();
        }

        public Task<Vaccine?> GetVaccineWithDoseRecords(int id)
        {
            throw new NotImplementedException();
        }
    }
}