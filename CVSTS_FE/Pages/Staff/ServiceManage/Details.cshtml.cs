using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BOs.Models;
using BOs.ResponseModels.Service;
using System.Net.Http.Headers;
using BOs.ResponseModels.Child;

namespace CVSTS_FE.Pages.Staff.ServiceManage
{
    public class DetailsModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DetailsModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public ServiceResponseModel Service { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id) 
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = CreateAuthorizedClient();
            var response = await client.GetAsync($"/api/Service/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            Service = await response.Content.ReadFromJsonAsync<ServiceResponseModel>();


            if (Service == null)
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
