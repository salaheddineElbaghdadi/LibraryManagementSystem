using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibraryManagementSystem.Models.Library;

namespace LibraryManagementSystem.ViewModels.Clients
{
    public class CreateNewClientViewModel
    {
        public Client client { get; set; }
        public List<ClientCategory> categories { get; set; }
    }
}