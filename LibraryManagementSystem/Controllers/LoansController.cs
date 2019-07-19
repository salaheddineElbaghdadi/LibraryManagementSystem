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
    }
}