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

        [HttpGet]
        public ActionResult EditBook(int id)
        {
            LibraryDal dal = new LibraryDal();
            EditBookViewModel model = new EditBookViewModel();
            model.bookId = id;
            model.book = dal.GetBookById(id);
            model.oldISBN = model.book.ISBN;

            if (model.book == null)
                return RedirectToAction("Index");

            return View(model);
        }

        [HttpPost]
        public ActionResult EditBook(EditBookViewModel model)
        {

            LibraryDal dal = new LibraryDal();
            Book newBook = new Book()
            {
                ISBN = model.book.ISBN,
                Title = model.book.Title,
                TotalQuantity = model.book.TotalQuantity,
                Archived = model.book.Archived
            };

            if (ModelState.IsValid)
            {
                if (model.oldISBN != newBook.ISBN)
                {
                    Book newIsbnBook = dal.GetBookByISBN(newBook.ISBN);
                    if (newIsbnBook != null)
                    {
                        model.alreadyExists = true;
                        return View(model);
                    }
                    dal.UpdateBook(dal.GetBookById(model.bookId), newBook);
                    return RedirectToAction("Index");
                }

                dal.UpdateBook(dal.GetBookById(model.bookId), newBook);
                return RedirectToAction("Index");
            }
            return View(model);
        }

    }
}