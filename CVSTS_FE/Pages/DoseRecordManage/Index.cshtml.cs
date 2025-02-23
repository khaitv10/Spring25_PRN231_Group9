using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BOs.Models;
using System.Net.Http.Headers;
using System.Net.Http;
using BOs.ResponseModels.Child;
using BOs.ResponseModels.DoseRecord;
using BOs.ResponseModels.Vaccine;

namespace CVSTS_FE.Pages.DoseRecordManage
{
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IList<DoseRecordResponseModel> DoseRecords { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            var client = CreateAuthorizedClient();
            var response = await APIHelper.GetAsJsonAsync<List<DoseRecordResponseModel>>(client, "/api/dose-record");

            if (response != null)
            {
                DoseRecords = response;
                return Page();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error occurred while logging in");
                return Redirect("/Login");
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
