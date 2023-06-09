using A_ZAcademy.Data;
using Microsoft.AspNetCore.Mvc;

namespace A_ZAcademy.ViewComponents
{
    public class MenuViewComponent:ViewComponent
    {
        private ApplicationDbContext db;
        public MenuViewComponent(ApplicationDbContext _db)
        {
            db = _db;
        }
        public IViewComponentResult Invoke()
        {
            var data = db.menus.OrderBy(x => x.CreationDate).Take(6);
            return View(data);
        }
    }
}
