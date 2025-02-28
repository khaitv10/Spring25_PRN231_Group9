using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BOs.Models;
using System.Net.Http.Headers;
using BOs.ResponseModels.DoseRecord;

namespace CVSTS_FE.Pages.DoseRecordManage
{

    public class EditModel : PageModel
    {
        [BindProperty]
        public DoseRecordResponseModel DoseRecord { get; set; } = default!;
        private readonly IHttpClientFactory _httpClientFactory;

        public EditModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = CreateAuthorizedClient();
            var response = await APIHelper.GetAsJsonAsync<DoseRecordResponseModel>(client, $"/api/dose-record/info/{id}");

            if (response != null)
            {
                DoseRecord = response;
                return Page();

            }
            return Redirect("./Index");
        }

        public async Task<IActionResult> OnPostAsync()
        {
           if (!ModelState.IsValid)
            {
                return Page();
            }

            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
            {
                return RedirectToPage("/403Page");
            }

              
            var id = DoseRecord.Id;
            var client = CreateAuthorizedClient();
            var response = await client.PutAsJsonAsync($"/api/dose-record/{id}", DoseRecord);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Error updating record.");
                return Page();
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
