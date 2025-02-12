using BOs.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class VaccineDAO
    {
        private readonly CvstsystemDbContext _context;

        public VaccineDAO(CvstsystemDbContext context)
        {
            _context = context;
        }

        public async Task<List<Vaccine>> GetAllVaccines()
        {
            return await _context.Vaccines.ToListAsync();
        }

        public async Task<Vaccine?> GetVaccineById(int id)
        {
            return await _context.Vaccines.FindAsync(id);
        }

        public async Task<Vaccine?> GetVaccineByName(string name)
        {
            return await _context.Vaccines.FirstOrDefaultAsync(v => v.Name == name);
        }

        public async Task AddVaccine(Vaccine vaccine)
        {
            _context.Vaccines.Add(vaccine);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateVaccine(Vaccine vaccine)
        {
            _context.Vaccines.Update(vaccine);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVaccine(int id)
        {
            var vaccine = await _context.Vaccines.FindAsync(id);
            if (vaccine != null)
            {
                _context.Vaccines.Remove(vaccine);
                await _context.SaveChangesAsync();
            }
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