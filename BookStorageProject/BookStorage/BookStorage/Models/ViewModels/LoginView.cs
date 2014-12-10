namespace BookStorage.Models.ViewModels
{
	using System.ComponentModel.DataAnnotations;

	public class LoginView
    {
        [Required(ErrorMessage = "Введите login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        public string Password { get; set; }

        public bool IsPersistent { get; set; }
    }
}