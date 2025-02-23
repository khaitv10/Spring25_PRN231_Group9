using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Service.Service.ChildServices;
using Service.Services.UserServices;

namespace CVSTSystem.Controllers.Odata
{
    [Route("odata/children")]
    [ApiController]
    public class ChildOdataController : ODataController
    {
        private readonly IChildService _childService;

        public ChildOdataController(IChildService childService)
        {
            _childService = childService;
        }

        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> GetAllChildren()
        {
            var response = await _childService.GetAllChilds();
            return Ok(response);
        }
    }
}
