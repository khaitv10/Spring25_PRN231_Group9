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
using BOs.RequestModels.VaccineStock;

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
        public VaccineStockUpdateModel VaccineStock { get; set; } = default!;
        [BindProperty]
        public int? Id { get; set; }

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
                Id = id;
                var date = response.ExpiryDate;
                VaccineStock = new VaccineStockUpdateModel();
                VaccineStock.ExpiryDate = DateTime.Parse(date.ToString());
                VaccineStock.Quantity = response.Quantity;

            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            var id = Id;
            var client = CreateAuthorizedClient();
            var response = await client.PutAsJsonAsync<VaccineStockUpdateModel>($"/api/vaccine/stock/{id}", VaccineStock);
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
