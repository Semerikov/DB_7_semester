namespace BookStorage.Models.Repositories
{
	using System.Linq;
	using DAL;

	public class UserRepository 
    {
		protected readonly BookShopDataBaseDataContext DbContext = new BookShopDataBaseDataContext();

        public User Login(string login, string password)
        {
			User result = DbContext.Users.FirstOrDefault(p => p.Email == login  && p.Pwd == password);
	        return result;
        }

        public User Login(string login)
        {
	        User result = DbContext.Users.FirstOrDefault(p => (string.Compare(p.Email, login, true) == 0));
            return result;
        }
    }
}