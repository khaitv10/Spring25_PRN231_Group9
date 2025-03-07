﻿using BOs.Models;
using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.DoseScheduleRepositories
{
    public class DoseScheduleRepository : GenericDAO<DoseSchedule>, IDoseScheduleRepository
    {
        public async Task AddDoseSchedule(DoseSchedule doseSchedule)
        {
            await Insert(doseSchedule);
        }

        public async Task DeleteDoseSchedule(int id)
        {
            var doseSchedule = await GetDoseScheduleById(id);
            if (doseSchedule != null)
                await Delete(doseSchedule);
        }

        public async Task<List<DoseSchedule>> GetAllDoseSchedule()
        {
            var list = await Get();
            return list.ToList();
        }

        public async Task<DoseSchedule> GetDoseScheduleById(int id)
        {
            return await GetSingle(b => b.Id == id);
        }

        public async Task UpdateDoseSchedule( int id, DoseSchedule doseSchedule)
        {
            await Update(doseSchedule);
        }
    }
}

