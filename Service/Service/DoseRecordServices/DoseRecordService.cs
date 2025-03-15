using AutoMapper;
using BOs.Models;
using BOs.RequestModels.DoseRecord;
using BOs.ResponseModels.DoseRecord;
using Repository.Repositories.DoseRecordRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service.DoseRecordServices
{
    public class DoseRecordService : IDoseRecordService
    {
        private readonly IDoseRecordRepository _doseRecordRepo;
        private readonly IMapper _mapper;

        public DoseRecordService(IDoseRecordRepository doseRecordRepo, IMapper mapper)
        {
            _doseRecordRepo = doseRecordRepo;
            _mapper = mapper;
        }

        public async Task AddDoseRecord(DoseRecordCreateModel doseRecord)
        {
            DoseRecord newDoseRecord = _mapper.Map<DoseRecord>(doseRecord);
            await _doseRecordRepo.AddDoseRecord(newDoseRecord);
        }

        public async Task DeleteDoseRecord(int id)
        {
            await _doseRecordRepo.DeleteDoseRecord(id);
        }

        public async Task<List<DoseRecordResponseModel>> GetAllDoseRecord()
        {
            var list = await _doseRecordRepo.GetAllDoseRecord();
            return _mapper.Map<List<DoseRecordResponseModel>>(list);
        }

        public async Task<DoseRecordResponseModel> GetByDoseRecordId(int id)
        {
            var doseSchedule = await _doseRecordRepo.GetByDoseRecordId(id);
            return _mapper.Map<DoseRecordResponseModel>(doseSchedule);
        }

        public async Task UpdateDoseRecord(int id, DoseRecordUpdateModel doseRecord)
        {
            var existingDoseRecord = await _doseRecordRepo.GetByDoseRecordId(id); 

            if (existingDoseRecord == null)
            {
                throw new Exception("Dose record not found");
            }

            _mapper.Map(doseRecord, existingDoseRecord);

            await _doseRecordRepo.UpdateDoseRecord(existingDoseRecord);
        }
    }
}
