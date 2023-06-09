using A_ZAcademy.Data;
using A_ZAcademy.Models;
using A_ZAcademy.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace A_ZAcademy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        /*public IActionResult CoursesByCategory(Category category)
		{
			var c = _context.courses.Where(x => x.Category == category);
			return View(c);
			//return View(_context.courses.Where(x=>x.Category!.CategoryName==category));
		}*/
        /*public async Task<IActionResult> CoursesByCategory(int? id)
        {
            var finalDbContext = _context.courses.Where(x => x.Category!.CategoryID == id).Include(c => c.Category);
            return View(await finalDbContext.ToListAsync());
        }*/
        public async Task<IActionResult> CoursesByCategory(int? id, int? pageNumber)
        {
            int pageSize = 6;

            var finalDbContext = _context.courses.Where(x => x.Category!.CategoryID == id).Include(c => c.Category);
            return View(PaginatedList<Course>.Create(await finalDbContext.ToListAsync(), pageNumber ?? 1, pageSize));
        }
        [HttpGet]
        public IActionResult RequestNewCourseForm()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RequestNewCourseForm(RequestNewCourseForm model)
        {
            if (ModelState.IsValid)
            {
                _context.requestNewCoursesForm.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }


            return View(model);
        }
        public async Task<IActionResult> BlogDetails(int? id)
        {
            if (id == null || _context.blogs == null)
            {
                return NotFound();
            }

            var blog = await _context.blogs
                .FirstOrDefaultAsync(m => m.BlogId == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }
        public IActionResult Search(string searchTerm)
        {
            // Perform the search query using the searchTerm
            var courses = _context.courses
                .Where(p => p.CourseTitle!.Contains(searchTerm) || p.CourseDescription!.Contains(searchTerm))
                .ToList();

            // Pass the results to the view or return them as needed
            return View(courses);
        }
        public async Task<IActionResult> Contact(Contact model)
        {
            if (ModelState.IsValid)
            {
                _context.contacts.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }


            return View(model);
        }
        public async Task<IActionResult> ConsumeInstL()
        {
            List<InstructorRequest> instructors = new List<InstructorRequest>();
            using (var httpClient = new HttpClient())
            {
                using (var result = await httpClient.GetAsync("https://localhost:7060/api/Instructors/GetAll"))
                {
                    string response=await result.Content.ReadAsStringAsync();
                    instructors=JsonConvert.DeserializeObject<List<InstructorRequest>>(response)!;
                }
            }
            return View(instructors);
        }

        //
        [HttpGet]
        public ActionResult JoinAsTeacher()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> JoinAsTeacher(InstructorRequest request)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://localhost:7060/api/"); // Update the base address

                var postJob = await httpClient.PostAsJsonAsync("Instructors/Post", request); // Correct the parameter name

                if (postJob.IsSuccessStatusCode)
                    return RedirectToAction("Index", "Home");

                ModelState.AddModelError(string.Empty, "Some Error");
            }

            return View(request);
        }

        // not working


        /* [HttpPost]
         public async Task<IActionResult> CreatConsumeInstL()
         {
             List<InstructorRequest> instructors = new List<InstructorRequest>();
             using (var httpClient = new HttpClient())
             {
                 var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7060/api/Instructors/Post");

                 using (var result = await httpClient.SendAsync(request))
                 {
                     string response = await result.Content.ReadAsStringAsync();
                     instructors = JsonConvert.DeserializeObject<List<InstructorRequest>>(response)!;
                 }
             }
             return View(instructors);
         }*/
        public async Task<IActionResult> GetInx()
        {
            List<InstructorRequest> instructorsR =new List<InstructorRequest>();
            using (var httpClient = new HttpClient())
            {
                using (var result=await httpClient.GetAsync("https://localhost:7060/api/Instructors/GetAll"))
                {
                    string response= await result.Content.ReadAsStringAsync();
                    instructorsR = JsonConvert.DeserializeObject<List<InstructorRequest>>(response)!;
                }
            }
            return View(instructorsR);
        }


    }
}