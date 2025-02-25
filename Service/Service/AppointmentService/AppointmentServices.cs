using AutoMapper;
using BOs.Models;
using BOs.RequestModels.Appointment;
using BOs.ResponseModels.Appointment;
using BOs.ResponseModels.Child;
using BOs.ResponseModels.Service;
using FFilms.Application.Shared.Response;
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

        public async Task<Result<Task>> DeleteAppointment(int appointId, int userId)
        {
            var app = await _appointmentRepository.GetDetailAppointment(appointId);
            if (app == null)
            {
                return new Result<Task>
                {
                    Success = false,
                    Message = "Appointment does not exist"
                };
            }
            if (app.Parent.Id != userId) {
                return new Result<Task>
                {
                    Success = false,
                    Message = "You do not have right to delete this appointment"
                };
            }
            if(app.Status == "Completed")
            {
                return new Result<Task>
                {
                    Success = false,
                    Message = "You can not delete completed appointment"
                };
            }
            app.Status = "Canceled";
            try
            {
                await _appointmentRepository.Update(app);
                return new Result<Task>
                {
                    Success = true,
                    Message = "Update Appointment status successfully"
                };
            }
            catch (Exception ex)
            {
                return new Result<Task>
                {
                    Success = false,
                    Message = "An exception: " + ex.Message
                };
            }

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

        public Task<Result<AppointmentResModel>> UpdateAppointment(AppointUpdateModel request, int userId, int appointId)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<Task>> UpdateAppointmentStatus(int appointId, string status)
        {
           var app = await _appointmentRepository.GetDetailAppointment(appointId);
            if(app == null)
            {
                return new Result<Task>
                {
                    Success = false,
                    Message = "Appointment does not exist"
                };
            }
            app.Status = status;
            try
            {
                await _appointmentRepository.Update(app);
                return new Result<Task>
                {
                    Success = true,
                    Message = "Update Appointment status successfully"
                };
            }
            catch (Exception ex) {
                return new Result<Task>
                {
                    Success = false,
                    Message = "An exception: " + ex.Message
                };
            }
        }
    }
}
