using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BOs.Models;
using BOs.ResponseModels.DoseRecord;
using System.Net.Http.Headers;
using Repository.Repositories.DoseScheduleRepositories;
using BOs.ResponseModels.DoseSchedule;
using BOs.RequestModels.DoseRecord;
using BOs.RequestModels.DoseSchedule;
using BOs.ResponseModels.Child;
using BOs.ResponseModels.Vaccine;

namespace CVSTS_FE.Pages.DoseSheduleManage
{
    public class EditModel : PageModel
    {
      

        //[BindProperty]
        //public DoseScheduleResponseModel DoseSchedule { get; set; } = default!;

        [BindProperty]
        public DoseScheduleUpdateModel DoseSchedules { get; set; } = default!;

        [BindProperty]
        public int? Id { get; set; } 



        private readonly IHttpClientFactory _httpClientFactory;

        public EditModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        public async Task<IActionResult> OnGetAsync(int? id)
        {
            

            if (id == null)
            {
                return NotFound();
            }

            var client = CreateAuthorizedClient();
            var response = await APIHelper.GetAsJsonAsync<DoseScheduleUpdateModel>(client, $"/api/dose-schedule/info/{id}");

            if (response != null)
            {
                DoseSchedules = response;
                id = id;
                return Page();

            }
            return Redirect("./Index");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            // {
            //     return Page();
            // }

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
            var response = await client.PutAsJsonAsync($"/api/dose-schedule/{id}", DoseSchedules);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Error updating schedule.");
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
