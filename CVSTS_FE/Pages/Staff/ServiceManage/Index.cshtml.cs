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

            var client = CreateAuthorizedClient();
            var url = $"/api/Service/getAllService";

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
