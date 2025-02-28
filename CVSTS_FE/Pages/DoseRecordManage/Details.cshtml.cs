using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BOs.Models;
using BOs.ResponseModels.DoseRecord;
using System.Net.Http.Headers;
using BOs.ResponseModels.Child;

namespace CVSTS_FE.Pages.DoseRecordManage
{
    public class DetailsModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DetailsModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public DoseRecordResponseModel DoseRecord { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return BadRequest("Invalid dose record ID.");
            }

            var client = CreateAuthorizedClient();
            var response = await client.GetAsync($"/api/dose-record/info/{id}"); 
            if (response != null)
                if (!response.IsSuccessStatusCode)
                {
                    return NotFound();
                }

            DoseRecord = await response.Content.ReadFromJsonAsync<DoseRecordResponseModel>();


            if (DoseRecord == null)
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
