using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BOs.Models;
using BOs.ResponseModels.VaccineStock;
using BOs.ResponseModels.Vaccine;
using System.Net.Http.Headers;

namespace CVSTS_FE.Pages.Staff.StockManage
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public IList<VaccineStockResponseModel> VaccineStock { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var client = _httpClientFactory.CreateClient("ApiClient");
            string jwt = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(jwt))
            {

                return Redirect("/Staff/index");
            }

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

            var response = await APIHelper.GetAsJsonAsync<List<VaccineStockResponseModel>>(client, "/api/Vaccine/stock");
            if(response != null)
            {
                VaccineStock = response;
                return Page();
            }
            ModelState.AddModelError(string.Empty, "Error occurred while calling");
            return Redirect("/Staff/");

        }
    }
}
