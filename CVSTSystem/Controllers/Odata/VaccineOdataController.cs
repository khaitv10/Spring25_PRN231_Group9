using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Service.Service.VaccineServices;
using Service.Services.UserServices;

namespace CVSTSystem.Controllers.Odata
{
    [Route("api/vaccines")]
    [ApiController]
    public class VaccineOdataController : ODataController
    {
        private readonly IVaccineService _vaccineService;

        public VaccineOdataController(VaccineService vaccineService)
        {
            _vaccineService = vaccineService;
        }

        [HttpGet]
        [EnableQuery]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllVaccines()
        {
            var response = await _vaccineService.GetAllVaccines();
            return Ok(response);
        }
    }
}
