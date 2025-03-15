using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BOs.Models;
using System.Net.Http.Headers;
using BOs.ResponseModels.DoseRecord;
using BOs.RequestModels.DoseRecord;
using BOs.ResponseModels.Vaccine;
using BOs.ResponseModels.Child;

namespace CVSTS_FE.Pages.DoseRecordManage
{
     
    public class EditModel : PageModel
    {
        //[BindProperty]
        //public DoseRecordResponseModel DoseRecord { get; set; } = default!;

        [BindProperty]
        public DoseRecordUpdateModel DoseRecords { get; set; } = default!;
        [BindProperty]
        public int? Id { get; set; }
        private readonly IHttpClientFactory _httpClientFactory;

        public EditModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            //var vaccineId = await APIHelper.GetAsJsonAsync<List<VaccineInfoResponseModel>>(CreateAuthorizedClient(), "/api/vaccine/active");
            //if (vaccineId != null)
            //{
            //    ViewData["VaccineId"] = new SelectList(vaccineId, "Id", "Name");
            //    var childId = await APIHelper.GetAsJsonAsync<List<ChildResponseModel>>(CreateAuthorizedClient(), "/api/child/getAllChild");
            //    if (childId != null)
            //    {
            //        ViewData["ChildId"] = new SelectList(childId, "Id", "FullName");
            //    }
            //}



            if (id == null)
            {
                return NotFound();
            }

            var client = CreateAuthorizedClient();
            var response = await APIHelper.GetAsJsonAsync<DoseRecordUpdateModel>(client, $"/api/dose-record/info/{id}");

            if (response != null)
            {
                DoseRecords = response;
                Id = id;
                return Page();

            }
            return Redirect("./Index");
        }

        public async Task<IActionResult> OnPostAsync()
{
    var userIdString = HttpContext.Session.GetString("UserId");
    if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
    {
        return RedirectToPage("/403Page");
    }

            var id = Id;

    if (id <= 0)
    {
        ModelState.AddModelError(string.Empty, "Error Id ");
        return Page();
    }

    var client = CreateAuthorizedClient();
    var response = await client.PutAsJsonAsync($"/api/dose-record/{id}", DoseRecords);

    if (!response.IsSuccessStatusCode)
    {
        //string errorMessage = await response.Content.ReadAsStringAsync();
        ModelState.AddModelError(string.Empty, $"Error updating record");
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
