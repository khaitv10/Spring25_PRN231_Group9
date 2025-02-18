using BOs.Models;
using Repository.Repositories.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.AppointmentRepositories
{
    public interface IAppointmentRepository: IGenericRepository<Appointment>
    {
        Task<List<Appointment>> GetAllAppointments();
        Task<Appointment> GetById(int id);
        Task<List<Appointment>> GetAllByParentId(int ParentId);
        Task<Appointment> GetDetailAppointment(int id);
    }

}
