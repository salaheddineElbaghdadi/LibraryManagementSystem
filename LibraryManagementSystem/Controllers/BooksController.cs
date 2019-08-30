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
        public ActionResult Index(string searchString)
        {
            
            LibraryDal dal = new LibraryDal();
            BooksViewModel model = new BooksViewModel();
            model.BookList = dal.BookList();


            if (!String.IsNullOrEmpty(searchString))
            {
                model.BookList = model.BookList.Where(b => b.Title.Contains(searchString) || b.ISBN.Contains(searchString)).ToList();
            }
            
            return View(model);
        }

        [HttpGet]
        public ActionResult CreateNew()
        {
            CreateNewViewModel model = new CreateNewViewModel();
            return View(model);
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

            if (ModelState.IsValid)
            {

                if (dal.GetBookByISBN(book.ISBN) != null)
                {
                    model.alreadyExists = true;
                    return View(model);
                }
                
                dal.AddNewBook(book);
                return Redirect("Index");
            }

            if (dal.GetBookByISBN(book.ISBN) != null)
                model.alreadyExists = true;
            else
                model.alreadyExists = false;

            return View(model);
        }


        public ActionResult Delete(int id)
        {
            LibraryDal dal = new LibraryDal();
            
            foreach (ClientBook loan in dal.LoansList())
            {
                if (loan.BookId == id)
                {
                    ViewBag.ErrorScript = @"<script>alert('This book is used in an active loan')</script>";
                    return View("Index", new BooksViewModel { BookList = dal.BookList() });
                }
            }

            ViewBag.ErroScript = String.Empty;
            dal.DeleteBook(dal.GetBookById(id).Id);

            return View("Index", new BooksViewModel { BookList = dal.BookList() });
        }

    }
}