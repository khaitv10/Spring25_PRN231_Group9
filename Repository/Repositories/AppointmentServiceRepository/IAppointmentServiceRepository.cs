using BOs.Models;
using Repository.Repositories.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.AppointmentServiceRepository
{
    public interface IAppointmentServiceRepository : IGenericRepository<AppointmentService>
    {
    }
}
