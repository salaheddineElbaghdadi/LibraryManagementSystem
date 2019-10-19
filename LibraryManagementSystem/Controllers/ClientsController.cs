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
        public ActionResult Index(string searchString)
        {
            ClientsViewModel model = new ClientsViewModel();
            LibraryDal dal = new LibraryDal();
            model.Clients = dal.ClientsList();

            if (!String.IsNullOrEmpty(searchString))
            {
                model.Clients = model.Clients.Where(c => c.FirstName.Contains(searchString) ||
                                                         c.LastName.Contains(searchString) ||
                                                         c.CIN.Contains(searchString)).ToList();
            }

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

            List<ClientCategory> categories = dal.GetClientCategories();
            List<SelectListItem> selections = new List<SelectListItem>();

            foreach (ClientCategory category in categories)
            {
                selections.Add(new SelectListItem { Text = category.ClientCategoryName, Value = category.Id.ToString() });
            }

            model.CategorySelection = selections;

            int id;
            Int32.TryParse(model.SelectedCategoryId, out id);
            model.client.Category = dal.GetClientCategory(id);

            if (model.client.Category != null)
            {
                ModelState.Remove("client.Category");
            }

            Client client = new Client()
            {
                FirstName = model.client.FirstName,
                LastName = model.client.LastName,
                CIN = model.client.CIN,
                Email = model.client.Email,
                Category = model.client.Category
            };

            if (ModelState.IsValid)
            {
                if (dal.GetClientByCIN(client.CIN) != null)
                {
                    model.validCIN = false;
                    return View(model);
                }
                dal.AddNewClient(client);
                return View("index", new ClientsViewModel() { Clients = dal.ClientsList() });
            }

            if (dal.GetClientByCIN(client.CIN) != null)
                model.validCIN = false;
            else
                model.validCIN = true;

            //model.client = client;
            return View(model);

        }

        public ActionResult Delete(int id)
        {
            LibraryDal dal = new LibraryDal();
            dal.DeleteClient(dal.GetClientById(id));

            return View("Index", new ClientsViewModel() { Clients = dal.ClientsList() });
        }

        public ActionResult AddLoan(int id)
        {
            return RedirectToAction("AddLoan", "Loans", new { clientId = id});
        }

        [HttpGet]
        public ActionResult EditClient(int id)
        {
            LibraryDal dal = new LibraryDal();
            EditClientViewModel model = new EditClientViewModel();
            model.clientId = id;
            model.client = dal.GetClientById(id);
            model.oldCIN = model.client.CIN;

            List<ClientCategory> categories = dal.GetClientCategories();
            List<SelectListItem> selections = new List<SelectListItem>();

            foreach (ClientCategory category in categories)
            {
                selections.Add(new SelectListItem { Text = category.ClientCategoryName, Value = category.Id.ToString() });
            }

            model.CategorySelection = selections;

            if (model.client == null)
                return RedirectToAction("Index");

            return View(model);
        }

        [HttpPost]
        public ActionResult EditClient(EditClientViewModel model)
        {
            LibraryDal dal = new LibraryDal();
           
            List<ClientCategory> categories = dal.GetClientCategories();
            List<SelectListItem> selections = new List<SelectListItem>();

            foreach (ClientCategory category in categories)
            {
                selections.Add(new SelectListItem { Text = category.ClientCategoryName, Value = category.Id.ToString() });
            }

            model.CategorySelection = selections;

            int id;
            Int32.TryParse(model.SelectedCategoryId, out id);
            model.client.Category = dal.GetClientCategory(id);

            if (model.client.Category != null)
            {
                ModelState.Remove("client.Category");
            }

            Client newClient = new Client
            {
                CIN = model.client.CIN,
                FirstName = model.client.FirstName,
                LastName = model.client.LastName,
                Email = model.client.Email,
                Category = model.client.Category
            };


            if (ModelState.IsValid)
            {
                if (model.oldCIN != newClient.CIN)
                {
                    Client newCINClient = dal.GetClientByCIN(newClient.CIN);
                    if (newCINClient != null)
                    {
                        model.alreadyExists = true;
                        return View(model);
                    }
                    dal.UpdateClient(dal.GetClientById(model.clientId), newClient);
                    return RedirectToAction("Index");
                }

                dal.UpdateClient(dal.GetClientById(model.clientId), newClient);
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
