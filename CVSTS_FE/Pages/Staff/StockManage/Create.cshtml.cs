using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BOs.Models;
using System.Net.Http.Headers;
using BOs.ResponseModels.Vaccine;

namespace CVSTS_FE.Pages.Staff.StockManage
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
            var response = await APIHelper.GetAsJsonAsync<List<VaccineInfoResponseModel>>(CreateAuthorizedClient(), "/api/vaccines");
            if (response != null)
            {
                ViewData["VaccineId"] = new SelectList(response, "Id", "Name");
                return Page();
            }
            return RedirectToPage("./Index");
        }

        [BindProperty]
        public VaccineStock VaccineStock { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {

            var client = CreateAuthorizedClient();
            var response = await APIHelper.PostAsJson<VaccineStock>(client, "/api/vaccine/stock", VaccineStock);
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Stock couldn't be added");
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
