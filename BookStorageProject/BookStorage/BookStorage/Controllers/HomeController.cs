using System.Web.Mvc;

namespace BookStorage.Controllers
{
	using Models.ViewModels;

	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View(new LoginView());
		}
	}
}