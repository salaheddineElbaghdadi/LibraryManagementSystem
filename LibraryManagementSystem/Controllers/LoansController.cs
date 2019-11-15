using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystem.Models.Library;
using LibraryManagementSystem.ViewModels.Loans;

namespace LibraryManagementSystem.Controllers
{
    public class LoansController : Controller
    {
        // GET: Loans
        public ActionResult Index(string searchString)
        {
            LibraryDal dal = new LibraryDal();
            LoansViewModel model = new LoansViewModel();

            model.Loans = dal.LoansList();
            model.Loans = model.Loans.OrderByDescending(l => l.StartDate).ToList();
            model.BookTitles = new List<string>();
            model.ClientsFirstNames = new List<string>();
            model.ClientsLastNames = new List<string>(); 

            foreach(ClientBook loan in model.Loans)
            {
                Book book = dal.GetBookById(loan.BookId);
                Client client = dal.GetClientById(loan.ClientId);
                model.BookTitles.Add(book.Title);
                model.ClientsFirstNames.Add(client.FirstName);
                model.ClientsLastNames.Add(client.LastName);
            }

            if (!String.IsNullOrEmpty(searchString))
            {
                model.Loans = model.Loans.Where(l => dal.GetBookById(l.BookId).Title.Contains(searchString) ||
                    dal.GetBookById(l.BookId).ISBN.Contains(searchString) ||
                    dal.GetClientById(l.ClientId).FirstName.Contains(searchString) ||
                    dal.GetClientById(l.ClientId).LastName.Contains(searchString) ||
                    dal.GetClientById(l.ClientId).CIN.Contains(searchString)).ToList();
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult AddLoan(int clientId)
        {
            LibraryDal dal = new LibraryDal();

            AddLoanViewModel model = new AddLoanViewModel();
            Client client = dal.GetClientById(clientId);

            model.loan = new ClientBook();
            model.loan.ClientId = clientId;
            model.loan.LoanDuration = client.Category.LoanDuration;

            return View(model);
        }

        [HttpPost]
        public ActionResult AddLoan(AddLoanViewModel model)
        {
            LibraryDal dal = new LibraryDal();
            Book book = dal.GetBookByISBN(model.ISBN);

            if (book != null)
            {
                model.bookFound = true;
                model.loan.BookId = book.Id;
                dal.AddLoan(model.loan);
                return RedirectToAction("Index");
            }
            else
            {
                model.bookFound = false;
            }

            return View(model);
        }

        public ActionResult EditLoan(int clientId, int bookId)
        {
            LibraryDal dal = new LibraryDal();
            EditLoanViewModel model = new EditLoanViewModel();

            model.clientId = clientId;
            model.bookId = bookId;
            model.loan = dal.GetLoan(clientId, bookId);

            return View(model);
        }

        [HttpPost]
        public ActionResult EditLoan(EditLoanViewModel model)
        {
            LibraryDal dal = new LibraryDal();
            ClientBook oldLoan = dal.GetLoan(model.clientId, model.bookId);

            if (ModelState.IsValid && oldLoan != null)
            {
                dal.UpdateLoan(oldLoan, model.loan);
                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}