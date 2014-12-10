namespace BookStorage.Models.BLL.Authentication
{
	using System;
	using System.Security.Principal;
	using DAL;
	using Repositories;

	public class UserIndentity : IIdentity
    {
        public User User { get; set; }

        public string AuthenticationType
        {
            get
            {
                return typeof(User).ToString();
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                return User != null;
            }
        }

        public string Name
        {
            get
            {
                if (User != null)
                {
                    return User.Person.Name;
                }
                return "anonym";
            }
        }

        public void Init(string login, UserRepository repository)
        {
            if (!string.IsNullOrEmpty(login))
            {
                User = repository.Login(login);
            }
        }

        public bool InRoles(string roles)
        {
            if (string.IsNullOrWhiteSpace(roles))
            {
                return false;
            }

            var rolesArray = roles.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var role in rolesArray)
            {
                if (User.Role.Value == role)
                {
                    return true;
                }
            }
            return false;
        }
    }
}