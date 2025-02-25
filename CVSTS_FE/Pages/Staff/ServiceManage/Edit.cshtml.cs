﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BOs.RequestModels.Service;
using BOs.ResponseModels.Vaccine;
using BOs.ResponseModels.Service;

namespace CVSTS_FE.Pages.Staff.ServiceManage
{
    public class EditModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public EditModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public ServiceUpdateModel Service { get; set; } = new();

        [BindProperty]
        public List<VaccineDoseModel> VaccineList { get; set; } = new();

        public List<VaccineInfoResponseModel> AllVaccines { get; set; } = new();

        public class VaccineDoseModel
        {
            public int VaccineId { get; set; }
            public int DoseCount { get; set; }
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            var client = CreateAuthorizedClient();

            // Fetch service details
            var serviceResponse = await client.GetAsync($"/api/Service/{id}");
            if (!serviceResponse.IsSuccessStatusCode) return NotFound();

            var service = await serviceResponse.Content.ReadFromJsonAsync<ServiceResponseModel>();
            if (service == null) return NotFound();

            // Populate service fields
            Service.Name = service.Name;
            Service.Description = service.Description;
            Service.Price = service.Price;
            Service.Status = service.Status;

            // Populate VaccineList
            if (service.Vaccine != null)
            {
                VaccineList = service.Vaccine
                    .Select(v => new VaccineDoseModel
                    {
                        VaccineId = v.VaccineInfo.Id,
                        DoseCount = v.NumberOfDose ?? 0
                    })
                    .ToList();
            }

            // Fetch all available vaccines
            var vaccineResponse = await client.GetAsync("/api/Vaccine");
            if (vaccineResponse.IsSuccessStatusCode)
            {
                AllVaccines = await vaccineResponse.Content.ReadFromJsonAsync<List<VaccineInfoResponseModel>>() ?? new();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to load available vaccines.");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid) return Page();

            // Convert VaccineList to a dictionary
            Service.Vaccines = VaccineList.ToDictionary(v => v.VaccineId, v => v.DoseCount);

            var client = CreateAuthorizedClient();
            var response = await client.PutAsJsonAsync($"/api/Service/{id}", Service);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("Index");
            }

            ModelState.AddModelError(string.Empty, "An error occurred while updating the service.");
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