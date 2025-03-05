using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BOs.Models;
using System.Net.Http.Headers;
using BOs.ResponseModels.Child;
using BOs.ResponseModels.Service;
using BOs.RequestModels.Service;

namespace CVSTS_FE.Pages.Staff.ServiceManage
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IList<ServiceResponseModel> Service { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var query = new ServiceQueryModel
            {
                Name = !string.IsNullOrEmpty(Request.Query["Name"]) ? Request.Query["Name"].ToString() : "",
                Status = bool.TryParse(Request.Query["Status"], out var status) ? (bool?)status : null,
                FromPrice = decimal.TryParse(Request.Query["FromPrice"], out var fromPrice) ? fromPrice : 0,
                ToPrice = decimal.TryParse(Request.Query["ToPrice"], out var toPrice) ? toPrice : 99999999.99M,
            };


            var client = CreateAuthorizedClient();
            var baseUrl = "/api/Service/getAllService";

            var queryParams = new List<string>();

            if (!string.IsNullOrEmpty(query.Name))
                queryParams.Add($"Name={Uri.EscapeDataString(query.Name)}");

            if (query.Status.HasValue)
                queryParams.Add($"Status={query.Status.Value}");

            if (query.FromPrice >=0)
                queryParams.Add($"FromPrice={query.FromPrice}");

            if (query.ToPrice <= 99999999.99M)
                queryParams.Add($"ToPrice={query.ToPrice}");

            var url = queryParams.Any() ? $"{baseUrl}?{string.Join("&", queryParams)}" : baseUrl;

            var response = await APIHelper.GetAsJsonAsync<List<ServiceResponseModel>>(client, url);

            if (response != null)
            {
                Service = response;
                return Page();
            }
            else
            {
                return RedirectToPage("/403Page");
            }
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
