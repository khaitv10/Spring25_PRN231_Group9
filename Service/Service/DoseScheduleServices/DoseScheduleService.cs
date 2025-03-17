using AutoMapper;
using BOs.Models;
using BOs.RequestModels.DoseSchedule;
using BOs.ResponseModels.DoseSchedule;
using Repository.Repositories.AppointmentRepositories;
using Repository.Repositories.DoseScheduleRepositories;
using Repository.Repositories.ServiceRepository;
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
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IServiceRepository _serviceRepository;
        public DoseScheduleService(IDoseScheduleRepository doseScheduleRepository, IMapper mapper, IAppointmentRepository appointmentRepository, IServiceRepository serviceRepository)
        {
            _doseScheduleRepository = doseScheduleRepository;
            _mapper = mapper;
            _appointmentRepository = appointmentRepository;
            _serviceRepository = serviceRepository;
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

        public async Task UpdateDoseSchedule(int id, DoseScheduleUpdateModel doseSchedule)
        {
            var dose = await _doseScheduleRepository.GetDoseScheduleById(id);
            _mapper.Map(doseSchedule, dose);
            await _doseScheduleRepository.Update(dose);
        }
        public async Task CreateScheduleOfAppoint(int appointId)
        {
            var appointment = await _appointmentRepository.GetDetailAppointment(appointId);
            var appointDate = DateOnly.FromDateTime(DateTime.UtcNow);
            var doseScheduleList = new List<DoseSchedule>();
            var doseNumber = 0;
            for (int i = 0; i < appointment.AppointmentServices.Count; i++)
            {
                var serviceList = await _serviceRepository.GetServiceById((int)appointment.AppointmentServices.ToList()[i].ServiceId);
                foreach (var item in serviceList.ServiceVaccines)
                    
                {
                    for (int j = 1; j<= item.NumberOfDose; i++)
                    {
                        appointDate = appointDate.AddMonths(i);
                        doseNumber++;
                        var doseSchedule = new DoseSchedule()
                        {
                            NextDoseDate = appointDate,
                            DoseNumber = doseNumber,
                            Status = "Scheduled",
                            CreateAt = DateTime.UtcNow,
                            ServiceId = item.ServiceId,
                            ChildId = appointment.ChildId,
                            VaccineId = item.VaccineId,

                        };
                        doseScheduleList.Add(doseSchedule);
                    }
                   
                }
                
            }
            await _doseScheduleRepository.InsertRange(doseScheduleList);

        }
    }
}
