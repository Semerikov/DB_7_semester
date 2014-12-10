namespace BookStorage.Controllers
{
	using System;
	using System.Web.Mvc;
	using Models.BLL.Authentication;
	using Models.DAL;
	using Ninject;

	public abstract class BaseController : Controller
    {
        [Inject]
        public IAuthentication Auth { get; set; }

        

        public User CurrentUser
        {
            get
            {
				Auth.HttpContext = HttpContext.ApplicationInstance.Context;
                return ((UserIndentity)Auth.CurrentUser.Identity).User;
            }
        }

        private String authorityName;

        public string AuthorityName
        { 
            get { return authorityName; }
            set { authorityName = value; }
        }

        public bool isAuth
        {
            get
            {
                if(CurrentUser==null)
                    return false;
                return AuthorityName.Equals(CurrentUser.Role.Value);
            }
        }

    }
}
