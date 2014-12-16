namespace BookStorage.Controllers
{
	using System;
	using System.Web.Mvc;
	using Models.Repositories;
	using Models.ViewModels;
    using BookStorage.Models.DAL;
    using System.Linq;

	public class LoginController : BaseController
    {
        private readonly BookShopDataBaseDataContext Context = new BookShopDataBaseDataContext();

        public ActionResult Index(LoginView loginView)
        {
            if (loginView!=null&&ModelState.IsValid)
            {
	            Auth.HttpContext = HttpContext.ApplicationInstance.Context;
                var user = Auth.Login(loginView.Login, loginView.Password, true);
                if (user != null)
                {
                    String area = "USER";
                    if (user.Role.Value.Equals("ADMIN"))
                    {
                        area = "ADMIN";
                    }
                        
                    return RedirectToAction("Index","Home",new {Area=area});
                }
                ModelState["Password"].Errors.Add("Пароли не совпадают");
            }
            
            return RedirectToAction("Index" , "Home");
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View(new RegistrationView());
        }

        [HttpPost]
		public ActionResult Registration(RegistrationView regView)
		{
            if (Context.Users.FirstOrDefault(user => user.Email == regView.Email) != null)
            {
                ModelState.AddModelError("Email", "Пользователем с таким email уже существует");
            }

            if (regView.CardNumber.Length != 16)
            {
                ModelState.AddModelError("CardNumber", "Номер карты должен содержать ровно 16 цифр");
            }

			if (ModelState.IsValid)
			{
                var newUser = new User
                {
                    CartNumber = regView.CardNumber,
                    Email = regView.Email,
                    Pwd = regView.Password,
                    Role = Context.Roles.Where(r => r.Value == "USER").Single(),
                    Person = new Person
                    {
                        Name = regView.Name,
                        Surname = regView.SurName,
                        BirthDay = DateTime.Now
                    }

                };
                Context.Users.InsertOnSubmit(newUser);
                Context.SubmitChanges();
				return RedirectToAction("Index", "Home");
			}

			return View(regView);
		}
        public ActionResult Logout()
        {
            Auth.LogOut();
            return RedirectToAction("Index","Login");
        }
    }
}