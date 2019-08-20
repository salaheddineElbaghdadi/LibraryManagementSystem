using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystem.Models.Library;
using LibraryManagementSystem.ViewModels.Books;

namespace LibraryManagementSystem.Controllers
{
    public class BooksController : Controller
    {
        // GET: Books
        public ActionResult Index()
        {
            
            LibraryDal dal = new LibraryDal();
            BooksViewModel model = new BooksViewModel();
            model.BookList = dal.BookList();
            
            return View(model);
        }

        public ActionResult CreateNew()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateNew(CreateNewViewModel model)
        {
            Book book = new Book()
            {
                Title = model.book.Title,
                ISBN = model.book.ISBN,
                TotalQuantity = model.book.TotalQuantity,
                Archived = model.book.Archived
            };

            LibraryDal dal = new LibraryDal();
            dal.AddNewBook(book);

            return View("Index", new BooksViewModel { BookList = dal.BookList() });
        }

        public ActionResult Delete(int id)
        {
            LibraryDal dal = new LibraryDal();
            dal.DeleteBook(dal.GetBookById(id).Id);

            return View("Index", new BooksViewModel { BookList = dal.BookList() });
        }

    }
}