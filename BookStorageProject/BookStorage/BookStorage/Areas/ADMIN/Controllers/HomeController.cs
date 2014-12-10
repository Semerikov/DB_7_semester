using System.Web.Mvc;

namespace BookStorage.Areas.ADMIN.Controllers
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Web.Routing;
	using BookStorage.Controllers;
	using Models.DAL;

	public class HomeController : BaseController
    {
		protected override void Initialize(RequestContext requestContext)
		{
			base.Initialize(requestContext);
			AuthorityName = "ADMIN";
		}

        // GET: ADMIN/Home
        public ActionResult Index()
        {
            return View();
        }

		public JsonResult UsersForGrid(string sidx, string sord, int page, int rows)
		{
			/*if (!isAuth)
				return new JsonResult();*/
			if (string.IsNullOrWhiteSpace(sidx))
				sidx = "Id";
			JqGridDataList jqGridDataList = new JqGridDataList();
			List<User> list = jqGridDataList.GetUsers(sidx, sord, page, rows);


			var jsonData = new
			{
				total = jqGridDataList.Total,
				page = jqGridDataList.Page,
				records = list.Count,
				rows = (
					from user in list
					select new
					{
						user.Id,
						user.Person.Name,
						user.Person.Surname,
						Role = user.Role.Value
					}
				).ToArray()
			};
			return Json(jsonData
				, JsonRequestBehavior.AllowGet);
		}
    }
}