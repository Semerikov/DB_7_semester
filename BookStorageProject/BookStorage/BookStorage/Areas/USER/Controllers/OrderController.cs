using System.Linq;
using System.IO.Compression;
using System.Web.Mvc;

namespace BookStorage.Areas.USER.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.IO;
	using BookStorage.Controllers;
	using BookStorage.Models.DAL;
    using BookStorage.Areas.USER.Models;

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

		public FileContentResult BuyBook(string bookIdsStr)
		{
            string[] bookIds = bookIdsStr.Split(',');
            List<byte[]> files = new List<byte[]>();

            User user = CurrentUser;
            Order newOrder = new Order() { Cost = 0, Creation_Date = DateTime.Now, User_Id = user.Id };
            Context.Orders.InsertOnSubmit(newOrder);
            Context.SubmitChanges();
            
            double cost = 0;
            foreach (var bookId in bookIds)
            {
                Book book = Context.Books.FirstOrDefault(b => b.ISBN == bookId);
                
                List<Double> userDisc = new List<double>(Context.Discounts.Where(d => d.User_Id == user.Id).Select(di => di.Value));
                double bookDisc = book.Cost.Discount != null ? book.Cost.Discount : 0;
                double bookCost = book.Cost.Value;
                cost *= (100.0 - bookDisc) / 100;
                foreach (double d in userDisc)
                {
                    bookCost *= (100.0 - d) / 100;
                }
                cost += bookCost;

                Context.Orders_Books.InsertOnSubmit(new Orders_Book() { Order_Id = newOrder.Id, Book_Id = book.ISBN });
                files.Add(FileHelper.GetFile(book.FilePath));
            }
            ViewBag.CostWithDiscounts = cost;
            newOrder.Cost = cost;

            Context.SubmitChanges();

            return File(CompressStringToFile(files), "application/zip", "zip.zip");
		}

        public ActionResult Details(int orderId)
        {
            var order = Context.Orders.Where(ord => ord.Id == orderId).Single();
            var orderBooks = Context.Orders_Books.Where(ob => ob.Order_Id == order.Id).Join(Context.Books, ob=>ob.Book_Id, b=>b.ISBN, (ob, b)=>b);

            var viewModel = new OrderDetailsView
            {
                Books = orderBooks.ToList(),
                Order = order
            };

            return View(viewModel);
        }

		public byte[] CompressStringToFile( List<byte[]> filestream)
        {

			using (var f2 = new MemoryStream())
			{
				GZipStream gz = new GZipStream(f2, CompressionMode.Compress, false);

				foreach (byte[] oStr in filestream)
				{
					byte[] b = oStr;
					gz.Write(b, 0, b.Length);

				}
				return f2.ToArray();
			}
        }}
    }
