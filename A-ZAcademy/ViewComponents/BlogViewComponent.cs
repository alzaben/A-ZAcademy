using A_ZAcademy.Data;
using Microsoft.AspNetCore.Mvc;

namespace A_ZAcademy.ViewComponents
{
    public class BlogViewComponent:ViewComponent
    {
        private ApplicationDbContext _db;
        public BlogViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }
        public IViewComponentResult Invoke()
        {
            var data = _db.blogs.OrderByDescending(x => x.CreationDate).Take(6);
            return View(data);
        }

    }
}
