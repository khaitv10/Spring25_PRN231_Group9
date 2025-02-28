using AutoMapper;
using BOs.Models;
using BOs.RequestModels.Appointment;
using BOs.ResponseModels.Appointment;
using BOs.ResponseModels.Child;
using BOs.ResponseModels.Service;
using FFilms.Application.Shared.Response;
using Repository.Repositories.AppointmentRepositories;
using Repository.Repositories.AppointmentServiceRepository;
using Repository.Repositories.ChildRepositories;
using Repository.Repositories.ServiceRepository;
using Repository.Repositories.VaccineRepositories;
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
        private readonly IServiceRepository _serviceRepository;
        private readonly IAppointmentServiceRepository _appointmentServiceRepository;
        private readonly IMapper _mapper;

        public AppointmentServices(IAppointmentRepository appointmentRepository, IServiceRepository serviceRepository, IAppointmentServiceRepository appointmentServiceRepository, IMapper mapper)
        {
            _serviceRepository = serviceRepository;
            _appointmentRepository = appointmentRepository;
            _appointmentServiceRepository = appointmentServiceRepository;
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
            if (app.ParentId != userId) {
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

        public async Task<Result<AppointmentResModel>> UpdateAppointment(AppointUpdateModel request, int userId, int appointId)
        {
            var app = await _appointmentRepository.GetDetailAppointment(appointId); 
            var appService = new List<BOs.Models.AppointmentService>();
            if (app == null)
            {
                return new Result<AppointmentResModel>
                {
                    Success = false,
                    Message = "Appointment does not exist"
                };
            }
            if (app.ParentId != userId)
            {
                return new Result<AppointmentResModel>
                {
                    Success = false,
                    Message = "You do not have right to update this appointment"
                };
            }
            if (app.Status == "Completed" || app.Status == "Canceled" )
            {
                return new Result<AppointmentResModel>
                {
                    Success = false,
                    Message = "You can not update completed/canceled appointment"
                };
            }
            decimal totalPrice = 0;
            
            foreach (var x in request.SelectedServiceIds) {
                var existService = await _serviceRepository.GetServiceById(x);
                if(existService == null || existService.Status == false)
                {
                    return new Result<AppointmentResModel>
                    {
                        Success = false,
                        Message = "Vaccine does not exist"
                    };
                }
                totalPrice += (decimal)existService.Price;
            }
            app.AppointmentDate = (DateTime)(request.AppointmentDate != null ? request.AppointmentDate : app.AppointmentDate);
            app.Type = "Vacciation";
            
            app.ChildId = request.ChildId != null ? request.ChildId : app.ChildId;
            if (app.PaymentStatus == "Paid")
            {
                await _appointmentRepository.Update(app);
                return new Result<AppointmentResModel>
                {
                    Success = true,
                    Message = "Appointment Date updated successfully. You can not update services in paid appointment",
                    Data = _mapper.Map<AppointmentResModel>(app)

                };
            }
            if (app.PaymentStatus != "Paid" || request.SelectedServiceIds.Count > 0) 
            {
                var appServiceRes = await _appointmentServiceRepository.Get(x => x.AppointmentId == appointId);
                app.TotalPrice = totalPrice;

                appService = appServiceRes.ToList();
                app.AppointmentServices = request.SelectedServiceIds
                  .Select(serviceId => new BOs.Models.AppointmentService
                  {
                      ServiceId = serviceId,
                      Appointment = app,
                      Status = "Pending",
                      DoseDate = request.AppointmentDate
                  }).ToList();
            }


            try
            {
                await _appointmentRepository.Update(app);
                await _appointmentServiceRepository.DeleteRange(appService);
                return new Result<AppointmentResModel>
                {
                    Success = true,
                    Message = "Update Successfully",
                    Data = _mapper.Map<AppointmentResModel>(app)

                };

            }
            catch (Exception ex) 
            {
                return new Result<AppointmentResModel>
                {
                    Success = false,
                    Message = "Update failed" + ex.Message
                };
            }

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
