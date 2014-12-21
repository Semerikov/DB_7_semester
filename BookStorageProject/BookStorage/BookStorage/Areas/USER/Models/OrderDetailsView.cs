using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookStorage.Models.DAL;

namespace BookStorage.Areas.USER.Models
{
    public class OrderDetailsView
    {
        public Order Order { get; set; }
        public List<Book> Books { get; set; }
    }
}