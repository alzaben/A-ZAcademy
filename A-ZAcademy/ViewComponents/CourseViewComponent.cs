using A_ZAcademy.Data;
using Microsoft.AspNetCore.Mvc;

namespace A_ZAcademy.ViewComponents
{
	public class CourseViewComponent:ViewComponent
	{
		private ApplicationDbContext db;
		public CourseViewComponent(ApplicationDbContext _db)
		{
			db = _db;
		}
		public IViewComponentResult Invoke()
		{
			var data = db.courses.OrderByDescending(x => x.CreationDate).Take(6);
			return View(data);
		}
	}
}
