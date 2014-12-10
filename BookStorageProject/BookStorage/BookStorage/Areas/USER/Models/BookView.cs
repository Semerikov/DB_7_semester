namespace BookStorage.Areas.USER.Models
{
	using System.Collections.Generic;
	using BookStorage.Models.DAL;

	public class BookView
	{
		public Book Book;

		public List<Author> Authors = new List<Author>();
	}
}