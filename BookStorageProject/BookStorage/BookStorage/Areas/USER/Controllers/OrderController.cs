using System.Linq;
using System.Web.Mvc;

namespace BookStorage.Areas.USER.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using BookStorage.Controllers;
	using BookStorage.Models.DAL;

	public class OrderController : BaseController
    {
		private readonly BookShopDataBaseDataContext Context = new BookShopDataBaseDataContext();

        public ActionResult Index(string bookIdsStr)
        {
            double cost = 0;
            string [] bookIds = bookIdsStr.Split(',');
            List<string> BookNames = new List<string>();
            foreach (var bookId in bookIds)
            {
                Book book = Context.Books.FirstOrDefault(b => b.ISBN == bookId);
                BookNames.Add(book.Name);
                User user = CurrentUser;
                ViewBag.User = user;
                List<Double> userDisc =
                    new List<double>(Context.Discounts.Where(d => d.User_Id == user.Id).Select(di => di.Value));
                double bookDisc = book.Cost.Discount != null ? book.Cost.Discount : 0;
                double bookCost = book.Cost.Value;
                bookCost *= (100.0 - bookDisc) / 100;
                foreach (double d in userDisc)
                {
                    bookCost *= (100.0 - d) / 100;
                }

                cost += bookCost;
            }
            ViewBag.BookNames = BookNames;
            ViewBag.BookIds = bookIdsStr;
			ViewBag.CostWithDiscounts = cost;
			return View();
	        
	        return RedirectToAction("Index", "Home");
        }

		public ActionResult BuyBook(string bookIdsStr)
		{
            string [] bookIds = bookIdsStr.Split(',');
            foreach (var bookId in bookIds)
            {
                Book book = Context.Books.FirstOrDefault(b => b.ISBN == bookId);
                User user = CurrentUser;
                List<Double> userDisc = new List<double>(Context.Discounts.Where(d => d.User_Id == user.Id).Select(di => di.Value));
                double bookDisc = book.Cost.Discount != null ? book.Cost.Discount : 0;
                double cost = book.Cost.Value;
                cost *= (100.0 - bookDisc) / 100;
                foreach (double d in userDisc)
                {
                    cost *= (100.0 - d) / 100;
                }
                ViewBag.CostWithDiscounts = cost;
                Context.Orders.InsertOnSubmit(new Order() { Book_Id = book.ISBN, Cost = cost, Creation_Date = DateTime.Now, User_Id = user.Id });
                Context.SubmitChanges();
            }
			return RedirectToAction("Index", "Home");
		}
    }
}