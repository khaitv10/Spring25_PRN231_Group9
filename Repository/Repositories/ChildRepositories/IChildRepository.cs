using BOs.Models;
using Repository.Repositories.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.ChildRepositories
{
    public interface IChildRepository : IGenericRepository<Child>
    {
        Task<List<Child>> GetAllChild();
        Task<Child> GetById(int id);
        Task<List<Child>> GetAllChildByParentId(int ParentId);
        Task<Child> GetDetailChild(int id);
       // IQueryable<Child> GetAllChildren();
    }

}
