using BOs.Models;
using Repository.Repositories.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.DoseScheduleRepositories
{
    public interface IDoseScheduleRepository : IGenericRepository<DoseSchedule>
    {
        Task<List<DoseSchedule>> GetAllDoseSchedule();
        Task<DoseSchedule> GetDoseScheduleById(int id);   
        Task AddDoseSchedule(DoseSchedule doseSchedule);
        Task UpdateDoseSchedule(DoseSchedule doseSchedule);
        Task DeleteDoseSchedule(int id);

    }
}
