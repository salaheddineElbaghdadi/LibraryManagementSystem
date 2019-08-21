using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibraryManagementSystem.Models.Library;

namespace LibraryManagementSystem.ViewModels.Books
{
    public class SearchBookViewModel
    {
        public string search { get; set; }
        public List<Book> books { get; set; }
    }
}