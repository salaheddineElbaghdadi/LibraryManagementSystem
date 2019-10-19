using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagementSystem.Models.Library;

namespace LibraryManagementSystem.ViewModels.Clients
{
    public class EditClientViewModel
    {

        public Client client
        {
            get { return _client; }
            set { _client = value; }
        }


        public int clientId { get; set; }
        public string oldCIN { get; set; }
        public bool alreadyExists { get; set; }
        public string SelectedCategoryId { get; set; }
        public List<SelectListItem> CategorySelection { get; set; }
        private Client _client;

        public EditClientViewModel()
        {
            alreadyExists = false;
        }
    }
}