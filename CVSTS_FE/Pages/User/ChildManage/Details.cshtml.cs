using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BOs.Models;
using BOs.ResponseModels.Child;
using System.Net.Http.Headers;
using System.Text.Json;

namespace CVSTS_FE.Pages.User.ChildManage
{
    public class DetailsModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DetailsModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public ChildDetailResModel Child { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == 0)
            {
                return BadRequest("Invalid child ID.");
            }

            var client = CreateAuthorizedClient();
            var response = await client.GetAsync($"/detail/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            Child = await response.Content.ReadFromJsonAsync<ChildDetailResModel>();


            if (Child == null)
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
