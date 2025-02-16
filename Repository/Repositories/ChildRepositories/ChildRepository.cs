using BOs.Models;
using DAO;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Child> GetDetailChild(int id)
        {
            var child = await GetSingle(
                a => a.Id == id,
                includeProperties: "DoseRecords.Vaccine,DoseSchedules.Vaccine"
            );

            if (child != null)
            {
                child.DoseRecords = child.DoseRecords
                    .Where(dr => dr.Status == "Completed")
                    .Select(dr => new DoseRecord
                    {
                        Id = dr.Id,
                        DoseDate = dr.DoseDate,
                        DoseNumber = dr.DoseNumber,
                        Status = dr.Status,
                        Vaccine = dr.Vaccine != null ? new Vaccine
                        {
                            Name = dr.Vaccine.Name,
                            Description = dr.Vaccine.Description
                        } : null
                    })
                    .ToList();

                child.DoseSchedules = child.DoseSchedules
                    .Where(ds => ds.Status == "Scheduled")
                    .Select(ds => new DoseSchedule
                    {
                        Id = ds.Id,
                        NextDoseDate = ds.NextDoseDate,
                        DoseNumber = ds.DoseNumber,
                        Status = ds.Status,
                        Vaccine = ds.Vaccine != null ? new Vaccine
                        {
                            Name = ds.Vaccine.Name,
                            Description = ds.Vaccine.Description
                        } : null
                    })
                    .ToList();
            }

            return child;
        }



    }
}
