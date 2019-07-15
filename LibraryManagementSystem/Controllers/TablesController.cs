using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystem.Models.Library;

namespace LibraryManagementSystem.Controllers
{
    public class TablesController : Controller
    {
        // GET: Tables
        public ActionResult Books()
        {
            // test
            Book book = new Book()
            {
                Title = "Test Book Title",
                ISBN = "17092307097",
                TotalQuantity = 12
            };

            LibraryDal dal = new LibraryDal();
            dal.AddNewBook(book);
            
            List<Book> BookList = dal.BookList();
            ViewBag.Books = BookList;

            return View();
        }

        public ActionResult AddBook()
        {
            return View();
        }
    }
}