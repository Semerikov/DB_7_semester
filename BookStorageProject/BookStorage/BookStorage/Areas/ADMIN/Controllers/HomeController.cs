using System.Web.Mvc;

namespace BookStorage.Areas.ADMIN.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Linq;
	using System.Web.Routing;
	using BookStorage.Controllers;
	using Models.DAL;

	public class HomeController : BaseController
    {
		private readonly BookShopDataBaseDataContext context = new BookShopDataBaseDataContext();
		protected override void Initialize(RequestContext requestContext)
		{
			base.Initialize(requestContext);
			AuthorityName = "ADMIN";
		}

        // GET: ADMIN/Home
        public ActionResult Index()
        {
            return View();
        }

		public JsonResult UsersForGrid(string sidx, string sord, int page, int rows)
		{
			if (!isAuth)
				return new JsonResult();
			if (string.IsNullOrWhiteSpace(sidx))
				sidx = "Id";
			JqGridDataList jqGridDataList = new JqGridDataList();
			List<User> list = jqGridDataList.GetUsers(sidx, sord, page, rows);


			var jsonData = new
			{
				total = jqGridDataList.Total,
				page ,
				records = list.Count,
				rows = (
					from user in list
					select new
					{
						user.Id,
						user.Person.Name,
						user.Person.Surname,
						Birthday = user.Person.BirthDay.ToString("yyyy MM dd"),
						user.Email,
						user.CartNumber,
						Role = user.Role.Value
					}
				).ToArray()
			};
			return Json(jsonData
				, JsonRequestBehavior.AllowGet);
		}

		public JsonResult UserUpdate(FormCollection form)
		{
			string operation = form["oper"];
			int Id = Convert.ToInt32(form["id"]);
			if ("edit".Equals(operation))
			{
				User user = context.Users.FirstOrDefault(u => u.Id.Equals(Id));
				Role role = context.Roles.FirstOrDefault(r => r.Value.Equals(form["Role"]));
				if (user != null&&role!=null)
				{
					user.Email = form["Email"];
					user.CartNumber = form["CartNumber"];
					if(CurrentUser.Id!=user.Id)
						user.Role_Id = role.Id;
					user.Person.Name = form["Name"];
					user.Person.Surname = form["Surname"];
					try
					{
						DateTime birthDay = new DateTime();
						DateTime.TryParseExact(form["Birthday"], "yyyy MM dd", CultureInfo.CurrentCulture,
						   DateTimeStyles.None,
						   out birthDay);
						user.Person.BirthDay = birthDay;
					}
					catch (Exception)
					{
						
					}
					context.SubmitChanges();
				}
			}
			if ("del".Equals(operation))
			{
				User user = context.Users.FirstOrDefault(u => u.Id.Equals(Id));
				context.Orders.DeleteAllOnSubmit(context.Orders.Where(o => o.User_Id.Equals(user.Id)));
				context.Discounts.DeleteAllOnSubmit(context.Discounts.Where(o => o.User_Id.Equals(user.Id)));
				context.Users.DeleteOnSubmit(user);
				context.Persons.DeleteOnSubmit(user.Person);
				context.SubmitChanges();
			}

			return Json(null
				, JsonRequestBehavior.AllowGet);
		}

		public JsonResult GetAllRoles()
		{
			if (!isAuth)
				return new JsonResult();
			
			List<Role> list = new List<Role>(context.Roles);
			var result = new
			{
				Roles = (
					from role in list
					select new
					{
						ID = role.Id,
						name = role.Value
					}
				).ToArray()
			};
			return Json(result
				, JsonRequestBehavior.AllowGet);
		}


		public JsonResult BookForGrid(string sidx, string sord, int page, int rows)
		{
			if (!isAuth)
				return new JsonResult();
			if (string.IsNullOrWhiteSpace(sidx))
				sidx = "ISBN";
			JqGridDataList jqGridDataList = new JqGridDataList();
			List<Book> list = jqGridDataList.GetBooks(sidx, sord, page, rows);


			var jsonData = new
			{
				total = jqGridDataList.Total,
				page ,
				records = list.Count,
				rows = (
					from books in list
					select new
					{
						books.ISBN,
						books.FilePath,
						books.ISBN_Tome,
						books.Description,
						NextPart = (books.Nextpart == null ? "" : books.Nextpart.Name),
						PreviousPart = (books.PreviousBook == null ? "" : books.PreviousBook.Name),
						books.RealiseYear,
						books.Cover,
						books.Name,
						Cost = (books.Cost != null) ? books.Cost.Value.ToString() : "",
						Authors = (from ab in books.Authors_Books
									select new
									{
										ab.Author_Id,
										Name = string.Format("{0} {1}", ab.Author.Person.Name, ab.Author.Person.Surname)
									})
                    
					}
				).ToArray()
			};
			return Json(jsonData
				, JsonRequestBehavior.AllowGet);
		}

		public JsonResult BookUpdate(FormCollection form)
		{
			string operation = form["oper"];
			if ("edit".Equals(operation) || "add".Equals(operation))
			{
				String ISBN = form["ISBN"];
				String FilePath = form["FilePath"];
				String ISBN_Tome = form["ISBN_Tome"];
				int RealiseYear = Convert.ToInt32(form["RealiseYear"]);
				String NextPartISBN = form["nextPartISBN"];
				String PreviousPartISBN = form["prevPartISBN"];
				String Description = form["Description"];
				String Name = form["Name"];
				String Cover = form["Cover"];
				double Cost = Convert.ToDouble(form["Cost"]);

				List<Int32> AuthorsId = new List<int>();
				string aIds = form["Authors"];
				if (!string.IsNullOrWhiteSpace(aIds))
				{
					String[] a = aIds.Split(',');
					foreach (string s in a)
					{
						AuthorsId.Add(Convert.ToInt32(s));
					}
				}
				Book b = null;
				if ("edit".Equals(operation))
					b = context.Books.FirstOrDefault(bo => bo.ISBN == ISBN);
				if (b == null)
				{
					b = new Book(){Cost = new Cost(){Discount = 0,Value = 0}};
					operation = "add";
				}
				b.ISBN = ISBN;
				b.Description = Description;
				b.Cover = Cover;
				b.Cost.Value = Cost;
				b.FilePath = FilePath;
				b.Name = Name;
				b.RealiseYear = RealiseYear;
				if (!string.IsNullOrWhiteSpace(NextPartISBN))
				{
					Book oldNextBook = context.Books.FirstOrDefault(bo => bo.ISBN == b.NextPart_Id);
					Book newNextBook = context.Books.FirstOrDefault(bo => bo.ISBN == NextPartISBN);
					if (oldNextBook != null)
					{
						oldNextBook.PreviousPart_Id = null;
					}
					if (newNextBook != null)
					{
						newNextBook.PreviousPart_Id = b.ISBN;
					}
					b.NextPart_Id = NextPartISBN;
				}
				else
				{
					
					Book oldNextBook = context.Books.FirstOrDefault(bo => bo.ISBN == b.NextPart_Id);
					if (oldNextBook != null)
					{
						oldNextBook.PreviousPart_Id = null;
					}
					b.NextPart_Id = null;
				}
				if (!string.IsNullOrWhiteSpace(PreviousPartISBN))
				{
					Book oldPrevBook = context.Books.FirstOrDefault(bo => bo.ISBN == b.PreviousPart_Id);
					Book newPrevBook = context.Books.FirstOrDefault(bo => bo.ISBN == PreviousPartISBN);
					if (oldPrevBook != null)
					{
						oldPrevBook.NextPart_Id = null;
					}
					if (newPrevBook != null)
					{
						newPrevBook.NextPart_Id = b.ISBN;
					}
					b.PreviousPart_Id = PreviousPartISBN;
				}
				else
				{
					
					Book oldPrevBook = context.Books.FirstOrDefault(bo => bo.ISBN == b.PreviousPart_Id);
					if (oldPrevBook != null)
					{
						oldPrevBook.NextPart_Id = null;
					}
					b.PreviousPart_Id = null;
				}
				if (b.ISBN_Tome!=null&&!b.ISBN_Tome.Equals(ISBN_Tome))
				{
					List<Book> tomBooks = new List<Book>(context.Books.Where(bo=>bo.ISBN_Tome.Equals(b.ISBN_Tome)));
					foreach (Book book in tomBooks)
					{
						book.ISBN_Tome = ISBN_Tome;
					}

				}
				b.ISBN_Tome = ISBN_Tome;

				foreach (int i in AuthorsId)
				{
					var bAth = new List<Authors_Book>(b.Authors_Books);
					if (!bAth.Any(a => a.Author_Id == i))
					{
						context.Authors_Books.InsertOnSubmit(new Authors_Book(){Author_Id = i,Book_Id = b.ISBN});
					}
				}
				if (AuthorsId.Count != b.Authors_Books.Count)
				{
					foreach (var i in b.Authors_Books)
					{
						if (!AuthorsId.Any(a => a == i.Author_Id))
						{
							context.Authors_Books.DeleteOnSubmit(i);
						}
					}
				}

				if ("add".Equals(operation))
				{
					context.Books.InsertOnSubmit(b);
				}
				
				context.SubmitChanges();
				
			}
			if ("del".Equals(operation))
			{
				String ISBN = form["Id"];
				context.Authors_Books.DeleteAllOnSubmit(context.Authors_Books.Where(a=>a.Book_Id == ISBN));
				context.Costs.DeleteAllOnSubmit(context.Costs.Where(c=>c.Book.ISBN.Equals(ISBN)));
				context.Books.DeleteOnSubmit(context.Books.FirstOrDefault(b=>b.ISBN.Equals(ISBN)));
				context.SubmitChanges();
			}

			return Json(null
				, JsonRequestBehavior.AllowGet);
		}

		public JsonResult GetBooks(string bookId, bool isNextPart)
		{
			if (!isAuth)
				return new JsonResult();
			Book currentBookPart;
			if(isNextPart)
				currentBookPart = context.Books.FirstOrDefault(b => b.ISBN.Equals(bookId)).Nextpart;
			else
				currentBookPart = context.Books.FirstOrDefault(b => b.ISBN.Equals(bookId)).PreviousBook;
			List<Book> list = new List<Book>(context.Books.Where(b=>!b.ISBN.Equals(bookId)));

			String currentPartISBN = string.Empty;
			if (currentBookPart != null)
				currentPartISBN = currentBookPart.ISBN;
			var jsonData = new
			{
				rows = (
					from book in list
					select new
					{
						book.Name,
						book.ISBN,
						isSelect = book.ISBN.Equals(currentPartISBN)
					}
				).ToArray()
			};
			return Json(jsonData
				, JsonRequestBehavior.AllowGet);
		}


		public JsonResult GetAuthors(string bookId)
		{
			if (!isAuth)
				return new JsonResult();
			List<Author> list = new List<Author>(context.Authors);
			List<Author> bookAuthors = new List<Author>(context.Authors.Where(b => b.Authors_Books.Any(ab => ab.Book_Id == bookId)));

			
			var jsonData = new
			{
				rows = (
					from a in list
					select new
					{
						Name = a.Person.Name +" "+ a.Person.Surname,
						a.Id,
						isSelect = bookAuthors.Any(ba=>ba.Id==a.Id)
					}
				).ToArray()
			};
			return Json(jsonData
				, JsonRequestBehavior.AllowGet);
		}

		public JsonResult GetAuthorBooks(string authorId)
		{
			if (!isAuth)
				return new JsonResult();
			List<Book> bookAuthors = new List<Book>();
			int aId = 0;
			if (!string.IsNullOrWhiteSpace(authorId) && authorId != "undefined")
			{
				aId = Convert.ToInt32(authorId);
				bookAuthors = new List<Book>(context.Books.Where(b => b.Authors_Books.Any(ab => ab.Author_Id == aId)));
			}
			List<Book> list = new List<Book>(context.Books);


			var jsonData = new
			{
				rows = (
					from a in list
					select new
					{
						a.Name ,
						a.ISBN,
						isSelect = bookAuthors.Any(ba => ba.ISBN == a.ISBN)
					}
				).ToArray()
			};
			return Json(jsonData
				, JsonRequestBehavior.AllowGet);
		}

		public JsonResult AuthorForGrid(string sidx, string sord, int page, int rows)
		{
			if (!isAuth)
				return new JsonResult();
			if (string.IsNullOrWhiteSpace(sidx))
				sidx = "Id";
			JqGridDataList jqGridDataList = new JqGridDataList();
			List<Author> list = jqGridDataList.GetAuthors(sidx, sord, page, rows);


			var jsonData = new
			{
				total = jqGridDataList.Total,
				page = page,
				records = list.Count,
				rows = (
					from a in list
					select new
					{
						AuthorId = a.Id,
						a.Person.Name,
						a.Person.Surname,
						Books = (from ab in a.Authors_Books
								   select new
								   {
									   ab.Book_Id,
									   ab.Book.Name
								   }).ToArray()
					}
				).ToArray()
			};
			return Json(jsonData
				, JsonRequestBehavior.AllowGet);
		}




		public JsonResult AuthorUpdate(FormCollection form)
		{
			string operation = form["oper"];
			if ("edit".Equals(operation) || "add".Equals(operation))
			{
				String Name = form["Name"];
				String Surname = form["Surname"];
				int Id = -1;
				if ("edit".Equals(operation))
				{
					Id = Convert.ToInt32(form["id"]);
				}
				String[] BooksIds = form["booksId"].Split(',');
				Author a = new Author(){Person = new Person()};
				if ("edit".Equals(operation))
				{
					a = context.Authors.First(au => au.Id == Id);
				}
				
				a.Person.Name = Name;
				a.Person.Surname = Surname;
				foreach (string i in BooksIds)
				{
					var bAth = new List<Authors_Book>(a.Authors_Books);
					if (!string.IsNullOrWhiteSpace(i)&&!bAth.Any(b => b.Book_Id == i))
					{
						context.Authors_Books.InsertOnSubmit(new Authors_Book() { Author_Id = a.Id, Book_Id = i });
					}
				}
				if (BooksIds.Count() != a.Authors_Books.Count)
				{
					foreach (var i in a.Authors_Books)
					{
						if (!BooksIds.Any(b => b == i.Book_Id))
						{
							context.Authors_Books.DeleteOnSubmit(i);
						}
					}
				}
				if ("add".Equals(operation))
				{					context.Authors.InsertOnSubmit(a);
				}

				context.SubmitChanges();
			}
			if ("del".Equals(operation))
			{
				int id = Convert.ToInt32(form["id"]);
				Author author = context.Authors.FirstOrDefault(b => b.Id.Equals(id));
				context.Authors_Books.DeleteAllOnSubmit(context.Authors_Books.Where(a=>a.Author_Id==id));
				context.Authors.DeleteOnSubmit(author);
				context.Persons.DeleteOnSubmit(author.Person);
				context.SubmitChanges();
			}

			return Json(null
				, JsonRequestBehavior.AllowGet);
		}

    }
}