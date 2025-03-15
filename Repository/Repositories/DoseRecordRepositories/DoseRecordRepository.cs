using BOs.Models;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.DoseRecordRepositories
{
    public class DoseRecordRepository : GenericDAO<DoseRecord>, IDoseRecordRepository
    {
        public async Task AddDoseRecord(DoseRecord doseRecord)
        {
            await Insert(doseRecord);
        }

        public async Task DeleteDoseRecord(int id)
        {
            var doseRecord = await GetByDoseRecordId(id);
            if (doseRecord != null)
                await Delete(doseRecord);
        }

        public async Task<List<DoseRecord>> GetAllDoseRecord()
        {
            var list = await Get(
                includeProperties: "Vaccine,Service,Child"
                );
            return list.ToList();
        }

        public async Task<DoseRecord> GetByDoseRecordId(int id)
        {
            return await GetSingle(
       x => x.Id == id,
       includeProperties: "Vaccine,Service,Child"
   );
        }

        public async Task UpdateDoseRecord(DoseRecord doseRecord)
        {
            await Update(doseRecord);
        }

        
    }
}
