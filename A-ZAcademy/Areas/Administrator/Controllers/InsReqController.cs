using A_ZAcademy.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

namespace A_ZAcademy.Areas.Administrator.Controllers
{
    [Area("Administrator")]
	[Authorize(Roles = "Admin")]
	public class InsReqController : Controller
    {
        public async Task<IActionResult> GetInx()
        {
            List<InstructorRequest> instructorsR = new List<InstructorRequest>();
            using (var httpClient = new HttpClient())
            {
                using (var result = await httpClient.GetAsync("https://localhost:7060/api/Instructors/GetAll"))
                {
                    string response = await result.Content.ReadAsStringAsync();
                    instructorsR = JsonConvert.DeserializeObject<List<InstructorRequest>>(response)!;
                }
            }
            return View(instructorsR);
        }
    }
}
