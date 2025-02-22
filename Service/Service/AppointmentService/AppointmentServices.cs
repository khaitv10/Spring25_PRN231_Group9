using AutoMapper;
using BOs.ResponseModels.Appointment;
using BOs.ResponseModels.Child;
using Repository.Repositories.AppointmentRepositories;
using Repository.Repositories.ChildRepositories;
using Service.Service.AppointmentServices;
using Service.Service.ChildServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service.AppointmentService
{
    public class AppointmentServices : IAppointmentServices
    {

        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMapper _mapper;

        public AppointmentServices(IAppointmentRepository appointmentRepository, IMapper mapper)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
        }
        public async Task<List<AppointmentResModel>> GetAllAppointments()
        {
            var list = await _appointmentRepository.GetAllAppointments();
            return _mapper.Map<List<AppointmentResModel>>(list);
        }

        public async Task<List<AppointmentResModel>> GetAllByParentId(int id)
        {
            var list = await _appointmentRepository.GetAllByParentId(id); 
            return _mapper.Map<List<AppointmentResModel>>(list);
        }

        public async Task<AppointmentResModel> GetAppointmentDetail(int id)
        {
            var app = await _appointmentRepository.GetDetailAppointment(id);
            return _mapper.Map<AppointmentResModel>(app);
        }
    }
}
