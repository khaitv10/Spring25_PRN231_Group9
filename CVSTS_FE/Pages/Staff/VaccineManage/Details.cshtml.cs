using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BOs.Models;
using System.Net.Http.Headers;
using BOs.ResponseModels.Vaccine;
using BOs.ResponseModels.VaccineStock;

namespace CVSTS_FE.Pages.VaccineManage
{
    public class DetailsModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DetailsModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public VaccineInfoResponseModel Vaccine { get; set; } = default!;
        public List<VaccineStockResponseModel> VaccineStocks { get; set; } = new List<VaccineStockResponseModel>();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var client = CreateAuthorizedClient();
            var response = await client.GetAsJsonAsync<VaccineInfoResponseModel>($"/api/vaccine/{id}");
            if (response != null)
            {
                Vaccine = response;

                // Fetch Vaccine Stocks
                var stockResponse = await client.GetAsJsonAsync<List<VaccineStockResponseModel>>($"/api/vaccine/stock/vaccine/{id}");
                if (stockResponse != null)
                {
                    VaccineStocks = stockResponse;
                }
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