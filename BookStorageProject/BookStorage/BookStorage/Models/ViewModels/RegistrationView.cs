namespace BookStorage.Models.ViewModels
{
	using System;
	using System.ComponentModel.DataAnnotations;

	public class RegistrationView
    {
		[Required(ErrorMessage = "Введите Name")]
		public String Name { get; set; }

		[Required(ErrorMessage = "Введите Email")]
		public String Email { get; set; }

		[Required(ErrorMessage = "Введите Password")]
		public String Password { get; set; }


    }
}