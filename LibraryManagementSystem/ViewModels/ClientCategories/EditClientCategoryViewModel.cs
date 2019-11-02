using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibraryManagementSystem.Models.Library;

namespace LibraryManagementSystem.ViewModels.ClientCategories
{
    public class EditClientCategoryViewModel
    {
        public ClientCategory clientCategory
        {
            get
            {
                return _clientCategory;
            }
            set
            {
                id = value.Id;
                _clientCategory = value;
            }
        }
        public int id { get; set; }
        private ClientCategory _clientCategory;
    }
}