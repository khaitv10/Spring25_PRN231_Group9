using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BOs.Models;
using BOs.ResponseModels.DoseSchedule;
using BOs.ResponseModels.DoseRecord;
using System.Net.Http.Headers;

namespace CVSTS_FE.Pages.DoseSheduleManage
{
    public class DetailsModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DetailsModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public DoseScheduleResponseModel DoseSchedule { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return BadRequest("Invalid dose shedule ID.");
            }

            var client = CreateAuthorizedClient();
            var response = await client.GetAsync($"/api/dose-shedule/info/{id}");
            if (response != null)
                if (!response.IsSuccessStatusCode)
                {
                    return NotFound();
                }

            DoseSchedule = await response.Content.ReadFromJsonAsync<DoseScheduleResponseModel>();


            if (DoseSchedule == null)
            {
                return NotFound();
            }

            return Page();
        }

        private HttpClient CreateAuthorizedClient()
        {
            var client = _httpClientFactory.CreateClient("ApiClient");
            var token = HttpContext.Session.GetString("JWToken");

            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return client;
        }
    }
}