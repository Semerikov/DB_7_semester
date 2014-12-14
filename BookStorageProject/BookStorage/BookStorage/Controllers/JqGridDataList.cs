using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;

namespace BookStorage.Controllers
{
	using Models.DAL;

	public class JqGridDataList
	{
		protected readonly BookShopDataBaseDataContext DbContext = new BookShopDataBaseDataContext();

		public int Total { get; set; }
		public int Page { get; set; }

		public List<Book> GetBooks(string sidx, string sord, int page, int rows)
		{
			return GetData<Book>(sidx, sord, page, rows, DbContext.Books);
		}

		public List<Author> GetAuthors(string sidx, string sord, int page, int rows)
		{
			return GetData<Author>(sidx, sord, page, rows, DbContext.Authors);
		}

		public List<User> GetUsers(string sidx, string sord, int page, int rows)
		{
			return GetData<User>(sidx, sord, page, rows, DbContext.Users);
		}

		public List<Order> GetOrdersByUser(string sidx, string sord, int page, int rows, int userId)
		{
			return GetData<Order>(sidx, sord, page, rows, DbContext.Orders.Where(o=>o.User_Id==userId));
		}

		public List<Book> GetBooksByAuthor(string sidx, string sord, int page, int rows , int authorId)
		{
			return GetData<Book>(sidx, sord, page, rows, DbContext.Books.Where(b=>b.Authors_Books.Any(ab=>ab.Author_Id==authorId)));
		}

		
		private List<T> GetData<T>(string sidx, string sord, int page, int rows , IQueryable<T> query)
		{
			if (string.IsNullOrEmpty(sidx))
			{
				sidx = "ID";
			}
			if (string.IsNullOrEmpty(sord))
			{
				sord = "asc";
			}
			query = query.OrderBy(sidx + " " + sord);
			List<T> list = new List<T>(query);
			Total = (int)Math.Ceiling((float)list.Count / rows);
			if (list.Count - (page) * rows > 0 && list.Count - (page) * rows < list.Count)
				list.RemoveRange((page) * rows, list.Count - (page) * rows);//delete from the end of page to the end of list
			list.RemoveRange(0, (page - 1) * rows);//delete from 0 to the begin of page
			return list;
		}
	}
}