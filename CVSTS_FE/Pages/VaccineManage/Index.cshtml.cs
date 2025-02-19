using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BOs.Models;
using FFilms.Application.Shared.Response;
using Repository.Enums;
using System.Security.Claims;
using BOs.ResponseModels.Vaccine;
using System.Net.Http.Headers;
using System.Net.Http;
using NuGet.Common;

namespace CVSTS_FE.Pages.VaccineManage
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public IList<VaccineInfoResponseModel> Vaccine { get;set; } = default!;

        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public async Task<IActionResult> OnGetAsync()
        {
            var client = _httpClientFactory.CreateClient("ApiClient");
            string jwt = HttpContext.Session.GetString("JWToken");
            if (string.IsNullOrEmpty(jwt))
            {
                // Handle the case where the JWT isn't available (e.g., redirect to login)
                return Redirect("/Login"); // Or appropriate action
            }

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

            var response = await APIHelper.GetAsJsonAsync<List<VaccineInfoResponseModel>>(client, "/api/Vaccine");

            if (response != null)
            {
                Vaccine = response;
                return Page();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error occurred while logging in");
                return Redirect("/Login");
            }
        }
    }
}
