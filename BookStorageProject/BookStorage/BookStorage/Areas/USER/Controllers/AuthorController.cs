using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace BookStorage.Areas.USER.Controllers
{
	using BookStorage.Controllers;
	using BookStorage.Models.DAL;
    using System.Drawing;

	public class AuthorController : Controller
    {

		private readonly BookShopDataBaseDataContext Context = new BookShopDataBaseDataContext();

        // GET: USER/Author
		public ActionResult Index(int authorId)
		{
			try
			{
				ViewBag.Author = Context.Authors.First(a => a.Id == authorId);
				return View();
			}
			catch (Exception)
			{
				return RedirectToAction("Index", "Home");
			}
		}

		public JsonResult BookForGrid(string sidx, string sord, int page, int rows , int authorId)
		{
			/*if (!isAuth)
				return new JsonResult();*/
			if (string.IsNullOrWhiteSpace(sidx))
				sidx = "ISBN";
			JqGridDataList jqGridDataList = new JqGridDataList();
			List<Book> list = jqGridDataList.GetBooksByAuthor(sidx, sord, page, rows , authorId);


			var jsonData = new
			{
				total = jqGridDataList.Total,
				page = jqGridDataList.Page,
				records = list.Count,
				rows = (
					from books in list
					select new
					{
						books.ISBN,
						books.Name,
						Cost = (books.Cost != null) ? books.Cost.Value.ToString() : ""
					}
				).ToArray()
			};
			return Json(jsonData
				, JsonRequestBehavior.AllowGet);
		}

        public ActionResult Photo(int authorId)
        {
            var filePath = Context.Authors.First(a => a.Id == authorId).Photo;
            byte[] fileBytes = FileHelper.GetFile(filePath);

            return File(fileBytes, "image/jpeg");
        }
    }
}