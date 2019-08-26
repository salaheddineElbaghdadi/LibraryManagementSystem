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
            model.ClientFirstNames = new List<string>();
            model.ClientLastNames = new List<string>(); 

            foreach(ClientBook loan in model.Loans)
            {
                Book book = dal.GetBookById(loan.BookId);
                Client client = dal.GetClientById(loan.ClientId);
                model.BookTitles.Add(book.Title);
                model.ClientFirstNames.Add(client.FirstName);
                model.ClientLastNames.Add(client.LastName);
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult AddLoan(int clientId)
        {
            AddLoanViewModel model = new AddLoanViewModel();
            model.loan = new ClientBook();
            model.loan.ClientId = clientId;

            return View(model);
        }

        [HttpPost]
        public ActionResult AddLoan(AddLoanViewModel model)
        {
            LibraryDal dal = new LibraryDal();
            dal.AddLoan(model.loan);

            return View("Index", new LoansViewModel { });
        }
    }
}