using A_ZAcademy.Data;
using Microsoft.AspNetCore.Mvc;

namespace A_ZAcademy.ViewComponents
{
    public class SliderViewComponent:ViewComponent
    {
        private ApplicationDbContext db;
        public SliderViewComponent(ApplicationDbContext _db)
        {
            db = _db;
        }
        public IViewComponentResult Invoke()
        {
            var data = db.sliders.OrderByDescending(x => x.CreationDate).Take(3);
            return View(data);
        }
    }
}
