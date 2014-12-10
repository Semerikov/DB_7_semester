namespace BookStorage.Models.BLL.Authentication
{
	using System;
	using System.Security.Principal;
	using System.Web;
	using System.Web.Security;
	using DAL;
	using Repositories;

	public class CustomAuthentication : IAuthentication
    {
        public UserRepository UserRepository = new UserRepository(); 

        private const string cookieName = "__AUTH_COOKIE";

		
        public HttpContext HttpContext { get; set; }

        #region IAuthenticated Members

        public User Login(string login, string password, bool isPersistent)
        {
            User retUser = UserRepository.Login(login, password);
            if (retUser != null)
            {
                CreateCookie(login, isPersistent);
            }
            return retUser;
        }

        public User Login(string login)
        {
            throw new NotImplementedException();
        }

        private void CreateCookie(string userName, bool isPersistent = false)
        {
			
            var ticket = new FormsAuthenticationTicket(
                  1,
                  userName,
                  DateTime.Now,
                  DateTime.Now.Add(FormsAuthentication.Timeout),
                  isPersistent,
                  string.Empty,
                  FormsAuthentication.FormsCookiePath);

            // Encrypt the ticket.
            var encTicket = FormsAuthentication.Encrypt(ticket);

            // Create the cookie.
            var authCookie = new HttpCookie(cookieName)
            {
                Value = encTicket,
                Expires = DateTime.Now.Add(FormsAuthentication.Timeout)
            };

            HttpContext.Response.Cookies.Set(authCookie);
        }

        public void LogOut()
        {
            var httpCookie = HttpContext.Response.Cookies[cookieName];
            if (httpCookie != null)
            {
                httpCookie.Value = string.Empty;
            }
        }

        private IPrincipal _currentUser;

        public IPrincipal CurrentUser
        {
            get
            {
                if (_currentUser == null || _currentUser.Identity.Name.Equals("anonym"))
                {
                    try
                    {
                        HttpCookie authCookie = HttpContext.Request.Cookies.Get(cookieName);
                        if (authCookie != null && !string.IsNullOrEmpty(authCookie.Value))
                        {
                            var ticket = FormsAuthentication.Decrypt(authCookie.Value);
                            _currentUser = new UserProvider(ticket.Name, UserRepository);
                        }
                        else
                        {
                            _currentUser = new UserProvider(null, null);
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine(ex.StackTrace);
                        _currentUser = new UserProvider(null, null);
                    }
                }
                return _currentUser;
            }
        }

        #endregion
    }
}