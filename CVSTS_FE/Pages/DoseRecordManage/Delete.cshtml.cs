﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BOs.Models;
using System.Net.Http.Headers;
using BOs.ResponseModels.DoseRecord;
using BOs.ResponseModels.Child;
using BOs.ResponseModels.Vaccine;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CVSTS_FE.Pages.DoseRecordManage
{
    public class DeleteModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DeleteModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        [BindProperty]
        public DoseRecordResponseModel DoseRecord { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {

           
            
            if (id == null)
            {
                return NotFound();
            }

            var client = CreateAuthorizedClient();
            var response =await client.GetAsJsonAsync<DoseRecordResponseModel>($"/api/dose-record/info/{id}");

            if (response != null)
            {
                DoseRecord = response;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
            {
                return RedirectToPage("/403Page");
            }
            var client = CreateAuthorizedClient();
            var response = await client.DeleteAsync($"/api/dose-record/{id}");
            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Error deleting record.");
                return Page();
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
