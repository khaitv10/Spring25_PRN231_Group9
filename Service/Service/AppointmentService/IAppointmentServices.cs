using BOs.Models;
using BOs.RequestModels.Appointment;
using BOs.RequestModels.Child;
using BOs.ResponseModels.Appointment;
using BOs.ResponseModels.Child;
using FFilms.Application.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service.AppointmentServices
{
    public interface IAppointmentServices
    {
        Task<AppointmentResModel> GetAppointmentDetail(int id);
        Task<List<AppointmentResModel>> GetAllAppointments();
        Task<List<AppointmentResModel>> GetAllByParentId(int id);
        Task CreateAppointment(AppointCreateModel request, int userId);
        //Task UpdateAppointment(int id, ChildUpdateModel request);
        //Task DeleteAppointment(int id);

    }

}
