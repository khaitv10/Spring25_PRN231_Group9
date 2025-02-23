using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BOs.Models;
using BOs.ResponseModels.VaccineStock;
using System.Net.Http.Headers;

namespace CVSTS_FE.Pages.Staff.StockManage
{
    public class EditModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EditModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public VaccineStockResponseModel VaccineStock { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var client = CreateAuthorizedClient();
            var response = await APIHelper.GetAsJsonAsync<VaccineStockResponseModel>(client, $"/api/vaccine/stock/{id}");
            if(response != null)
            {
                VaccineStock = response;
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var id = VaccineStock.Id;
            var client = CreateAuthorizedClient();
            var response = await client.PutAsJsonAsync<VaccineStockResponseModel>($"/api/vaccine/stock/{id}", VaccineStock);
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Stock cannot Updated");
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
