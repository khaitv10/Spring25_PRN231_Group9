using AutoMapper;
using BOs.Models;
using BOs.RequestModels.DoseSchedule;
using BOs.ResponseModels.DoseSchedule;
using Repository.Repositories.DoseScheduleRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service.DoseScheduleServices
{
    public class DoseScheduleService : IDoseScheduleService
    {
        private readonly IDoseScheduleRepository _doseScheduleRepository;
        private readonly IMapper _mapper;

        public DoseScheduleService(IDoseScheduleRepository doseScheduleRepository, IMapper mapper)
        {
            _doseScheduleRepository = doseScheduleRepository;
            _mapper = mapper;
        }

        public async Task AddDoseSchedule(DoseScheduleCreateModel doseSchedule)
        {
            DoseSchedule newDoseSchedule  = _mapper.Map<DoseSchedule>(doseSchedule);
            await _doseScheduleRepository.AddDoseSchedule(newDoseSchedule);
        }

        public async Task DeleteDoseSchedule(int id)
        {
            await _doseScheduleRepository.DeleteDoseSchedule(id);
        }

        public async Task<List<DoseScheduleResponseModel>> GetAllDoseSchedule()
        {
            var list = await _doseScheduleRepository.GetAllDoseSchedule();
            return _mapper.Map<List<DoseScheduleResponseModel>>(list);
        }

        public async Task<DoseScheduleResponseModel> GetDoseScheduleById(int id)
        {
            var doseSchedule = await _doseScheduleRepository.GetDoseScheduleById(id);
            return _mapper.Map<DoseScheduleResponseModel>(doseSchedule);
        }

        public async Task UpdateDoseSchedule(DoseScheduleUpdateModel doseSchedule)
        {
            DoseSchedule dose = _mapper.Map<DoseSchedule>(doseSchedule);
            await _doseScheduleRepository.UpdateDoseSchedule(dose);
        }
    }
}
