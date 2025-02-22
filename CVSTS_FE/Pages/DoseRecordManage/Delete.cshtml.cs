using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BOs.Models;
using System.Net.Http.Headers;

namespace CVSTS_FE.Pages.DoseRecordManage
{
    public class DeleteModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DeleteModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        [BindProperty]
        public DoseRecord DoseRecord { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return BadRequest("Invalid dose record id");
            }

            var client = CreateAuthorizedClient();
            var response =await client.GetAsync($"/info/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            DoseRecord = await response.Content.ReadFromJsonAsync<DoseRecord>();

            if (DoseRecord == null) { return NotFound(); };
           
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (DoseRecord == null)
            {
                return NotFound();
            }

            var id = DoseRecord.Id;
            var client = CreateAuthorizedClient();
            var response = await client.DeleteAsync($"/api/dose-record/{id}");
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Error deleting dose record");
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
