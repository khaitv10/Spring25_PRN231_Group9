using BOs.Models;
using DAO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.AppointmentRepositories
{
    public class AppointmentRepository : GenericDAO<Appointment>, IAppointmentRepository
    {
        public async Task<List<Appointment>> GetAllAppointments()
        {
            var list = await Get();
            return list.ToList();
        }

        public async Task<List<Appointment>> GetAllByParentId(int ParentId)
        {
            var list = await Get(
                filter: b => b.ParentId == ParentId,
                includeProperties: "Child" 
            );
            return list.ToList();
        }


        public async Task<Appointment> GetById(int id)
        {
            return await GetSingle(a => a.Id == id);
        }

        public async Task<Appointment> GetDetailAppointment(int id)
        {
            return await GetSingle(
                a => a.Id == id,
                includeProperties: "AppointmentServices.Service,Child"
            );
        }


    }
}
