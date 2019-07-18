using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibraryManagementSystem.Models.Library;

namespace LibraryManagementSystem.ViewModels.Books
{
    public class BooksViewModel
    {
        public List<Book> BookList { get; set; }
    }
}