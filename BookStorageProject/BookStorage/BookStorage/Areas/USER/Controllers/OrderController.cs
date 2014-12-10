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

        public ActionResult Index(string bookId)
        {
	        
		        try
		        {
			        Book book = Context.Books.FirstOrDefault(b => b.ISBN == bookId);
			        User user = CurrentUser;
			        ViewBag.User = user;
			        ViewBag.Book = book;
			        List<Double> userDisc =
				        new List<double>(Context.Discounts.Where(d => d.User_Id == user.Id).Select(di => di.Value));
			        double bookDisc = book.Cost.Discount != null ? book.Cost.Discount : 0;
			        double cost = book.Cost.Value;
			        cost *= (100.0 - bookDisc)/100;
			        foreach (double d in userDisc)
			        {
				        cost *= (100.0 - d)/100;
			        }
			        ViewBag.CostWithDiscounts = cost;
			        return View();
		        }
		        catch (Exception)
		        {

		        }
	        
	        return RedirectToAction("Index", "Home");
        }

		public ActionResult BuyBook(string bookId)
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
			Context.Orders.InsertOnSubmit(new Order(){Book_Id = book.ISBN,Cost = cost,Creation_Date = DateTime.Now,User_Id = user.Id});
			Context.SubmitChanges();
			return RedirectToAction("Index", "Home");
		}
    }
}