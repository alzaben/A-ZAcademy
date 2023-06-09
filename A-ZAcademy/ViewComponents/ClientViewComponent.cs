using A_ZAcademy.Data;
using Microsoft.AspNetCore.Mvc;

namespace A_ZAcademy.ViewComponents
{
    public class ClientViewComponent:ViewComponent
    {
        private ApplicationDbContext db;
        public ClientViewComponent(ApplicationDbContext _db)
        {
            db = _db;
        }
        public IViewComponentResult Invoke()
        {
            var data = db.clients.OrderByDescending(x => x.CreationDate).Take(3);
            return View(data);
        }
    }
}
