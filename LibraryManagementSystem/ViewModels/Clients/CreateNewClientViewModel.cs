using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystem.Models.Library;

namespace LibraryManagementSystem.ViewModels.Clients
{
    public class CreateNewClientViewModel
    {
        public Client client { get; set; }
        public List<SelectListItem> CategorySelection { get; set; }
        public int SelectedCategoryId { get; set; }
    }
}