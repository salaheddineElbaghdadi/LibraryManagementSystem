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
        public ActionResult Index()
        {
            LibraryDal dal = new LibraryDal();
            LoansViewModel model = new LoansViewModel();

            model.Loans = dal.LoansList();
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
            model.loan.BookId = book.Id;

            dal.AddLoan(model.loan);


            //return View("Index");
            return RedirectToAction("Index");
        }
    }
}