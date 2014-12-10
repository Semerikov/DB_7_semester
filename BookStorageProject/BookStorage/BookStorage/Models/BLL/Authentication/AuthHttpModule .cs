namespace BookStorage.Models.BLL.Authentication
{
	using System;
	using System.Web;
	using System.Web.Mvc;

	public class AuthHttpModule : IHttpModule
    {
        public IAuthentication Auth { get; set; }     

        public void Init(HttpApplication context)
        {
            context.AuthenticateRequest += new EventHandler(this.Authenticate);
        }

        private void Authenticate(Object source, EventArgs e)
        {
            HttpApplication app = (HttpApplication)source;
            HttpContext context = app.Context;

            var auth = DependencyResolver.Current.GetService<IAuthentication>();//null
            
            auth.HttpContext = context;
            context.User = auth.CurrentUser;
        }

        public void Dispose()
        {
        }
    }
}