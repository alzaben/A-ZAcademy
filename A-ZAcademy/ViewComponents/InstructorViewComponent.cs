using A_ZAcademy.Data;
using Microsoft.AspNetCore.Mvc;

namespace A_ZAcademy.ViewComponents
{
    public class InstructorViewComponent:ViewComponent
    {

        private ApplicationDbContext db;
        public InstructorViewComponent(ApplicationDbContext _db)
        {
            db = _db;
        }
        public IViewComponentResult Invoke()
        {
            var data = db.instructors.OrderByDescending(x => x.CreationDate).Take(4);
            return View(data);
        }
    }
}
