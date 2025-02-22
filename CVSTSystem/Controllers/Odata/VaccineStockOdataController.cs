using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Service.Service.VaccineStockServices;

namespace CVSTSystem.Controllers.Odata
{
    [Route("api/vaccine-stock")]
    [ApiController]
    public class VaccineStockOdataController : ODataController
    {
        private readonly IVaccineStockService _vaccineStockService;

        public VaccineStockOdataController(IVaccineStockService vaccineStockService)
        {
            _vaccineStockService = vaccineStockService;
        }
        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> GetAllVaccineStocks()
        {
            var response = await _vaccineStockService.GetAllVaccineStocks();
            return Ok(response);
        }
    }
}
