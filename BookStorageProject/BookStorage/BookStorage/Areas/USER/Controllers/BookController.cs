using System.Linq;
using System.Web.Mvc;

namespace BookStorage.Areas.USER.Controllers
{
	using System.Collections.Generic;
	using BookStorage.Models.DAL;
	using Models;

	public class BookController : Controller
    {
		private readonly BookShopDataBaseDataContext Context = new BookShopDataBaseDataContext();
        // GET: USER/Book
		public ActionResult Index(string bookId)
		{
			if (string.IsNullOrWhiteSpace(bookId))
				return RedirectToAction("Index", "Home");
			BookView view = new BookView()
			{
				Book = Context.Books.FirstOrDefault(b => b.ISBN.Equals(bookId)),
				Authors =new List<Author>(Context.Books.FirstOrDefault(b => b.ISBN.Equals(bookId)).Authors_Books.Select(c=>c.Author))
			
			};
			ViewBag.BookView = view;
            return View();
        }
    }
}