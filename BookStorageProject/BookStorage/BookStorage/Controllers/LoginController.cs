namespace BookStorage.Controllers
{
	using System;
	using System.Web.Mvc;
	using Models.Repositories;
	using Models.ViewModels;

	public class LoginController : BaseController
    {
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


        public ActionResult Logout()
        {
            Auth.LogOut();
            return RedirectToAction("Index","Login");
        }
    }
}