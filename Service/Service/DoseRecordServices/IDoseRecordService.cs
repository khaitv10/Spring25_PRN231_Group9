using BOs.Models;
using BOs.RequestModels.DoseRecord;
using BOs.RequestModels.DoseSchedule;
using BOs.ResponseModels.DoseRecord;
using BOs.ResponseModels.DoseSchedule;
using Repository.Repositories.DoseRecordRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service.DoseRecordServices
{
    public interface IDoseRecordService
    {
        Task<List<DoseRecordResponseModel>> GetAllDoseRecord();
        Task<DoseRecordResponseModel> GetByDoseRecordId(int id);
        Task AddDoseRecord(DoseRecordCreateModel doseRecord);
        Task UpdateDoseRecord(int id ,DoseRecordUpdateModel doseRecord);
        Task DeleteDoseRecord(int id);
        
    }
}
