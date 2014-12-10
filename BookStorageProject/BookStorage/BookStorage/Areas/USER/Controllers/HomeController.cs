using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BookStorage.Areas.USER.Controllers
{
	using System.Web.Routing;
	using BookStorage.Controllers;
	using BookStorage.Models.DAL;

	public class HomeController : BaseController
    {
		protected override void Initialize(RequestContext requestContext)
		{
			base.Initialize(requestContext);
			AuthorityName = "USER";
		}
        // GET: USER/Home
        public ActionResult Index()
        {
            return View();
        }

		public JsonResult BookForGrid(string sidx, string sord, int page, int rows)
		{
			/*if (!isAuth)
				return new JsonResult();*/
			if (string.IsNullOrWhiteSpace(sidx))
				sidx = "ISBN";
			JqGridDataList jqGridDataList = new JqGridDataList();
			List<Book> list = jqGridDataList.GetBooks(sidx, sord, page, rows);

			
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
						Cost = (books.Cost != null )?books.Cost.Value.ToString() : ""
					}
				).ToArray()
			};
			return Json(jsonData
				, JsonRequestBehavior.AllowGet);
		}

		public JsonResult OrdersForGrid(string sidx, string sord, int page, int rows)
		{
			if (!isAuth)
				return new JsonResult();
			if (string.IsNullOrWhiteSpace(sidx))
				sidx = "Id";
			JqGridDataList jqGridDataList = new JqGridDataList();
			List<Order> list = jqGridDataList.GetOrdersByUser(sidx, sord, page, rows,CurrentUser.Id);


			var jsonData = new
			{
				total = jqGridDataList.Total,
				page = jqGridDataList.Page,
				records = list.Count,
				rows = (
					from order in list
					select new
					{
						order.Id,
						Creation_Date=order.Creation_Date.ToString(),
						order.Book.Name,
						order.Cost
					}
				).ToArray()
			};
			return Json(jsonData
				, JsonRequestBehavior.AllowGet);
		}
    }
}