using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BOs.Models;
using System.Net.Http.Headers;
using BOs.ResponseModels.DoseSchedule;
using BOs.RequestModels.DoseSchedule;
using BOs.ResponseModels.Child;
using BOs.ResponseModels.Vaccine;

namespace CVSTS_FE.Pages.DoseSheduleManage
{
    public class CreateModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CreateModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> OnGet()
        {
            var response = await APIHelper.GetAsJsonAsync<List<VaccineInfoResponseModel>>(CreateAuthorizedClient(), "/api/vaccine/active");
            if (response != null)
            {
                ViewData["VaccineId"] = new SelectList(response, "Id", "Name");
                var childId = await APIHelper.GetAsJsonAsync<List<ChildResponseModel>>(CreateAuthorizedClient(), "/api/child/getAllChild");
                if (childId != null)
                {
                    ViewData["ChildId"] = new SelectList(childId, "Id", "FullName");
                    return Page();
                }

            }

            return RedirectToPage("./Index");
        }

        [BindProperty]
        public DoseScheduleCreateModel DoseSchedule { get; set; } = default!;


     

        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                var vaccineId = await APIHelper.GetAsJsonAsync<List<VaccineInfoResponseModel>>(CreateAuthorizedClient(), "/api/vaccine/active");
                if (vaccineId != null)
                {
                    ViewData["VaccineId"] = new SelectList(vaccineId, "Id", "Name");
                }

                var childId = await APIHelper.GetAsJsonAsync<List<ChildResponseModel>>(CreateAuthorizedClient(), "/api/child/getAllChild");
                if (childId != null)
                {
                    ViewData["ChildId"] = new SelectList(childId, "Id", "FullName");
                }

                return Page();
            }
            var client = CreateAuthorizedClient();
            var response = await client.PostAsJsonAsync<DoseScheduleCreateModel>($"/api/dose-schedule", DoseSchedule);
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Error create schedule");
            }

            return RedirectToPage("./Index");
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