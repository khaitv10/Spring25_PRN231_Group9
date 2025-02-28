using BOs.Models;
using BOs.RequestModels.Child;
using BOs.RequestModels.DoseSchedule;
using BOs.ResponseModels.Child;
using BOs.ResponseModels.DoseSchedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service.DoseScheduleServices
{
    public interface IDoseScheduleService
    {
        Task<List<DoseScheduleResponseModel>> GetAllDoseSchedule();
        Task<DoseScheduleResponseModel> GetDoseScheduleById(int id);
        Task AddDoseSchedule(DoseScheduleCreateModel doseSchedule);
        Task UpdateDoseSchedule(int id,DoseScheduleUpdateModel doseSchedule);
        Task DeleteDoseSchedule(int id);
    }
}
