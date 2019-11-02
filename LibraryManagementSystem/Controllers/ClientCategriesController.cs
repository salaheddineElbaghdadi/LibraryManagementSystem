using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystem.Models.Library;
using LibraryManagementSystem.ViewModels.ClientCategories;

namespace LibraryManagementSystem.Controllers
{
    public class ClientCategoriesController : Controller
    {
        public ActionResult Index()
        {

            LibraryDal dal = new LibraryDal();
            ClientCategoriesViewModel model = new ClientCategoriesViewModel();
            model.ClientCategoies = dal.GetClientCategories();

            return View(model);
        }

        public ActionResult CreateNewCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateNewCategory(CreateNewCategoryViewModel model)
        {
            ClientCategory category = new ClientCategory()
            {
                ClientCategoryName = model.Category.ClientCategoryName,
                LoanDuration = model.Category.LoanDuration,
                MaxLoans = model.Category.MaxLoans
            };

            LibraryDal dal = new LibraryDal();
            dal.AddNewClientCategory(category);

            return View("Index", new ClientCategoriesViewModel { ClientCategoies = dal.GetClientCategories() });
        }

        public ActionResult Delete(int id)
        {
            LibraryDal dal = new LibraryDal();
            dal.DeleteClientCategory(dal.GetClientCategory(id).Id);

            return View("Index", new ClientCategoriesViewModel { ClientCategoies = dal.GetClientCategories() });
        }


        [HttpGet]
        public ActionResult EditClientCategory(int id)
        {
            LibraryDal dal = new LibraryDal();
            EditClientCategoryViewModel model = new EditClientCategoryViewModel();

            model.clientCategory = dal.GetClientCategory(id);

            return View(model);
        }

        [HttpPost]
        public ActionResult EditClientCategory(EditClientCategoryViewModel model)
        {
            LibraryDal dal = new LibraryDal();

            

            if (ModelState.IsValid)
            {
                ClientCategory newCategroy = new ClientCategory
                {
                    ClientCategoryName = model.clientCategory.ClientCategoryName,
                    LoanDuration = model.clientCategory.LoanDuration,
                    MaxLoans = model.clientCategory.MaxLoans
                };

                dal.UpdateClientCategory(dal.GetClientCategory(model.id), newCategroy);
                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}