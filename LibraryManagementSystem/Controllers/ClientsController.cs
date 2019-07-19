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

            List<ClientCategory> categories = dal.GetClientCategories();
            List<SelectListItem> selections = new List<SelectListItem>();

            foreach (ClientCategory category in categories)
            {
                selections.Add(new SelectListItem { Text = category.ClientCategoryName, Value = category.Id.ToString() });
            }

            model.CategorySelection = selections;

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateNewClient(CreateNewClientViewModel model)
        {
            LibraryDal dal = new LibraryDal();

            Client client = new Client()
            {
                FirstName = model.client.FirstName,
                LastName = model.client.LastName,
                CIN = model.client.CIN,
                Email = model.client.Email,
                Category = dal.GetClientCategory(model.SelectedCategoryId)
            };

            dal.AddNewClient(client);

            return View("index", new ClientsViewModel() { Clients = dal.ClientsList() });
        }

        public ActionResult Delete(int id)
        {
            LibraryDal dal = new LibraryDal();
            dal.DeleteClient(dal.GetClientById(id));

            return View("Index", new ClientsViewModel() { Clients = dal.ClientsList() });
        }
    }
}