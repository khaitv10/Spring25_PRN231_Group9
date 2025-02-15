using BOs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.DoseRecordRepositories
{
    public interface IDoseRecordRepository
    {
        Task<List<DoseRecord>> GetAllDoseRecord();
        Task<DoseRecord> GetByDoseRecordId(int id);
        Task AddDoseRecord(DoseRecord doseRecord);
        Task UpdateDoseRecord(DoseRecord doseRecord);
        Task DeleteDoseRecord(int id);
    }
}
