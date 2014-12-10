using System.Web.Mvc;

namespace BookStorage.Areas.USER
{
    public class USERAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "USER";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "USER_default",
                "USER/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}