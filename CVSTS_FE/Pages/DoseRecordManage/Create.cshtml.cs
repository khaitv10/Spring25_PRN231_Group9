using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BOs.Models;
using BOs.RequestModels.DoseRecord;
using System.Net.Http.Headers;
using BOs.ResponseModels.Vaccine;
using BOs.ResponseModels.DoseRecord;
using BOs.ResponseModels.Child;

namespace CVSTS_FE.Pages.DoseRecordManage
{
    public class CreateModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CreateModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async  Task<IActionResult> OnGet()
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
            //return Page();

        }

        [BindProperty]
        public DoseRecordCreateModel DoseRecord { get; set; } = default!;

        [BindProperty]
        public DoseRecordResponseModel DoseRecords { get; set; } = default!;
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            var client = CreateAuthorizedClient();
            var response = await client.PostAsJsonAsync<DoseRecordCreateModel>($"/api/dose-record", DoseRecord);
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Error create dose record");
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
