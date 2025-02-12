using BOs.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class VaccineDAO : GenericDAO<Vaccine>
    {
        private readonly CvstsystemDbContext _context;

        private static VaccineDAO instance = null;

        public static VaccineDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new VaccineDAO();
                }
                return instance;
            }
        }

        public VaccineDAO()
        {
            _context = new CvstsystemDbContext();
        }

        public async Task<Vaccine?> GetVaccineByName(string name)
        {
            return await _context.Vaccines.FirstOrDefaultAsync(v => v.Name == name);
        }


        public async Task<bool> IsVaccineNameExists(string name)
        {
            return await _context.Vaccines.AnyAsync(v => v.Name == name);
        }

        public async Task<List<Vaccine>> GetVaccinesByAgeRange(int minAge, int maxAge)
        {
            return await _context.Vaccines.Where(v => v.MinAge <= maxAge && v.MaxAge >= minAge).ToListAsync();
        }


        public async Task<Vaccine?> GetVaccineWithDoseRecords(int id)
        {
            return await _context.Vaccines
                .Include(v => v.DoseRecords)  
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<List<Vaccine>> GetActiveVaccines()
        {
            return await _context.Vaccines.Where(v => v.Status == true).ToListAsync();
        }


    }
}