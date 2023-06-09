using A_ZAcademy.Data;
using Microsoft.AspNetCore.Mvc;

namespace A_ZAcademy.ViewComponents
{
    public class CategoryViewComponent:ViewComponent
    {
        private ApplicationDbContext db;
        public CategoryViewComponent(ApplicationDbContext _db)
        {
            db = _db;
        }
        public IViewComponentResult Invoke()
        {
            var data = db.categories.OrderByDescending(x => x.CreationDate).Take(8);
            return View(data);
        }
    }
}
