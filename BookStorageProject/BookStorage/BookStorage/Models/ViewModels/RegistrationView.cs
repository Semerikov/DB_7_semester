namespace BookStorage.Models.ViewModels
{
	using System;
	using System.ComponentModel.DataAnnotations;

	public class RegistrationView
    {
		[Required(ErrorMessage = "Введите имя")]
		public String Name { get; set; }

        [Required(ErrorMessage = "Введите фамилию")]
        public String SurName { get; set; }

		[Required(ErrorMessage = "Введите Email")]
		public String Email { get; set; }

		[Required(ErrorMessage = "Введите пароль")]
		public String Password { get; set; }

        [Required(ErrorMessage = "Введите номер вашей карты")]
        public String CardNumber { get; set; }
    }
}