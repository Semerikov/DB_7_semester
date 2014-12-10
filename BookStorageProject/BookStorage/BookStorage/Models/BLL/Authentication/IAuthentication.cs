namespace BookStorage.Models.BLL.Authentication
{
	using System.Security.Principal;
	using System.Web;
	using DAL;

	public interface IAuthentication
    {
        HttpContext HttpContext { get; set; }

        User Login(string login, string password, bool isPersistent);

        User Login(string login);

        void LogOut();

        IPrincipal CurrentUser { get; }
    }
}
