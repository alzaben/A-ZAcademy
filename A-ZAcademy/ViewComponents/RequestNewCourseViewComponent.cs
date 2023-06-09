using A_ZAcademy.Data;
using Microsoft.AspNetCore.Mvc;

namespace A_ZAcademy.ViewComponents
{
    public class RequestNewCourseViewComponent : ViewComponent
    {
        private ApplicationDbContext db;
        public RequestNewCourseViewComponent(ApplicationDbContext _db)
        {
            db = _db;
        }
        public IViewComponentResult Invoke()
        {
            var data = db.requestNewCourses.OrderByDescending(x => x.CreationDate).Take(1);
            return View(data);
        }
    }
}