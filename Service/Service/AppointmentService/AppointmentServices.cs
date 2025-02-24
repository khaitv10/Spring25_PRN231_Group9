using AutoMapper;
using BOs.Models;
using BOs.RequestModels.Appointment;
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

        public async Task CreateAppointment(AppointCreateModel request, int userId)
        {
            Appointment app = _mapper.Map<Appointment>(request);
            app.ParentId = userId;
            app.Status = "Scheduled";
            app.PaymentStatus = "Unpaid";

            if (request.SelectedServiceIds != null && request.SelectedServiceIds.Any())
            {
                app.AppointmentServices = request.SelectedServiceIds
                    .Select(serviceId => new BOs.Models.AppointmentService
                    {
                        ServiceId = serviceId,
                        Appointment = app,  
                        Status = "Pending", 
                        DoseDate = request.AppointmentDate
                    }).ToList();
            }

            await _appointmentRepository.Insert(app);
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
