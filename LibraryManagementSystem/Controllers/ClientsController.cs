using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystem.ViewModels.Clients;
using LibraryManagementSystem.Models.Library;

namespace LibraryManagementSystem.Controllers
{
    public class ClientsController : Controller
    {
        // GET: Clients
        public ActionResult Index()
        {
            ClientsViewModel model = new ClientsViewModel();
            LibraryDal dal = new LibraryDal();
            model.Clients = dal.ClientsList();

            return View(model);
        }

        public ActionResult CreateNewClient()
        {
            CreateNewClientViewModel model = new CreateNewClientViewModel();
            LibraryDal dal = new LibraryDal();
            model.categories = dal.GetClientCategories();

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateNewClient(CreateNewClientViewModel model)
        {
            Client client = new Client()
            {
                FirstName = model.client.FirstName,
                LastName = model.client.LastName,
                CIN = model.client.CIN,
                Email = model.client.Email,
            };

            LibraryDal dal = new LibraryDal();
            dal.AddNewClient(client);

            return View("index", new ClientsViewModel() { Clients = dal.ClientsList() });
        }
    }
}