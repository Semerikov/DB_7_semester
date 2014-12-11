using System.Web.Mvc;

namespace BookStorage.Areas.ADMIN.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Linq;
	using System.Web.Routing;
	using BookStorage.Controllers;
	using Models.DAL;

	public class HomeController : BaseController
    {
		private readonly BookShopDataBaseDataContext context = new BookShopDataBaseDataContext();
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
			if (!isAuth)
				return new JsonResult();
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
						Birthday = user.Person.BirthDay.ToString("yyyy MM dd"),
						user.Email,
						user.CartNumber,
						Role = user.Role.Value
					}
				).ToArray()
			};
			return Json(jsonData
				, JsonRequestBehavior.AllowGet);
		}

		public JsonResult UserUpdate(FormCollection form)
		{
			string operation = form["oper"];
			int Id = Convert.ToInt32(form["id"]);
			if ("edit".Equals(operation))
			{
				User user = context.Users.FirstOrDefault(u => u.Id.Equals(Id));
				Role role = context.Roles.FirstOrDefault(r => r.Value.Equals(form["Role"]));
				if (user != null&&role!=null)
				{
					user.Email = form["Email"];
					user.CartNumber = form["CartNumber"];
					if(CurrentUser.Id!=user.Id)
						user.Role_Id = role.Id;
					user.Person.Name = form["Name"];
					user.Person.Surname = form["Surname"];
					try
					{
						DateTime birthDay = new DateTime();
						DateTime.TryParseExact(form["Birthday"], "yyyy MM dd", CultureInfo.CurrentCulture,
						   DateTimeStyles.None,
						   out birthDay);
						user.Person.BirthDay = birthDay;
					}
					catch (Exception)
					{
						
					}
					context.SubmitChanges();
				}
			}
			if ("del".Equals(operation))
			{
				User user = context.Users.FirstOrDefault(u => u.Id.Equals(Id));
				context.Users.DeleteOnSubmit(user);
				//context.SubmitChanges();
			}

			return Json(null
				, JsonRequestBehavior.AllowGet);
		}

		public JsonResult GetAllRoles()
		{
			if (!isAuth)
				return new JsonResult();
			
			List<Role> list = new List<Role>(context.Roles);
			var result = new
			{
				Roles = (
					from role in list
					select new
					{
						ID = role.Id,
						name = role.Value
					}
				).ToArray()
			};
			return Json(result
				, JsonRequestBehavior.AllowGet);
		}
    }
}