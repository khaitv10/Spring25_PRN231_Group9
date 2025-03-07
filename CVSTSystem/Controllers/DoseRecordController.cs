﻿using BOs.Models;
using BOs.RequestModels.DoseRecord;
using BOs.ResponseModels.DoseRecord;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Service.DoseRecordServices;

namespace CVSTSystem.Controllers
{
    [Route("api/dose-record")]
    [ApiController]
    public class DoseRecordController : ControllerBase
    {
        private readonly IDoseRecordService _recordService;

        public DoseRecordController(IDoseRecordService recordService)
        {
            _recordService = recordService;
        }

        [HttpGet]
        
        public async Task<ActionResult<List<DoseRecordResponseModel>>> GetAllDoseRecrd()
        {
            var doseRecords = await _recordService.GetAllDoseRecord();
            return Ok(doseRecords);
        }

        [HttpGet]
        [Route("info/{id}")]
        public async Task<ActionResult<DoseRecordResponseModel>> GetDoseRecord(int id)
        {
            var doseRecord = await _recordService.GetByDoseRecordId(id);
            if (doseRecord == null) { return NotFound(); }
            return Ok(doseRecord);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Staff")]

        public async Task<ActionResult> DeleteDoseRecord(int id)
        {
            await _recordService.DeleteDoseRecord(id);
            return Ok("Delete dose record successfully");
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "Staff")]

        public async Task<ActionResult<DoseRecordUpdateModel>> UpdateRecord(int id,DoseRecordUpdateModel doseRecord) 
        {
            await _recordService.UpdateDoseRecord(id, doseRecord);

            return Ok("Update record successfully");
        }

        private bool DoseRecordExists(int id)
        {
            return _recordService.GetByDoseRecordId(id) != null;
        }

        [HttpPost]
        [Authorize(Roles = "Staff")]

        public async Task<ActionResult<DoseRecordCreateModel>> CreateDoseRecord([FromBody] DoseRecordCreateModel doseRecord) 
        {
            if(!ModelState.IsValid)
            {
                BadRequest();   
            }
            await _recordService.AddDoseRecord(doseRecord);
            return Ok("Create new record successfully");
        }
    }
}
