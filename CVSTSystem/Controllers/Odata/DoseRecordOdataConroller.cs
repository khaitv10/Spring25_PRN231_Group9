using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Service.Service.DoseRecordServices;

namespace CVSTSystem.Controllers.Odata
{
    [Route("odata/records")]
    [ApiController]
    public class DoseRecordOdataConroller : ODataController
    {
        private readonly IDoseRecordService _recordService;

        public DoseRecordOdataConroller(IDoseRecordService recordService)
        {
            _recordService = recordService;
        }

        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> GetAllRecord() 
        {
            var record = await _recordService.GetAllDoseRecord();
            return Ok(record);
        }
    }
}
