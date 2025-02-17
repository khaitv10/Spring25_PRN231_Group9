using BOs.Models;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.ChildRepositories
{
    public class ChildRepository : GenericDAO<Child>, IChildRepository
    {
        public async Task<List<Child>> GetAllChild()
        {
            var list = await Get();
            return list.ToList();
        }

        public async Task<List<Child>> GetAllChildByParentId(int ParentId)
        {
            var list = await Get(b => b.ParentId == ParentId);
            return list.ToList();
        }

        public async Task<Child> GetById(int id)
        {
            return await GetSingle(a => a.Id == id);
        }
    }
}
