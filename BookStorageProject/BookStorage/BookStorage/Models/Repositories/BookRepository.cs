using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStorage.Models.Repositories
{
	using System.Data.Linq;
	using DAL;

	public class BookRepository
	{
		protected readonly BookShopDataBaseDataContext DbContext = new BookShopDataBaseDataContext();

		public Table<Book> GetAll()
		{
			return DbContext.Books;
		}
	}
}