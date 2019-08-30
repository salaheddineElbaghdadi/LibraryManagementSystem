using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibraryManagementSystem.Models.Library;

namespace LibraryManagementSystem.ViewModels.Books
{
    public class CreateNewViewModel
    {
        public bool alreadyExists { get; set; }
        public Book book { get; set; }

        public CreateNewViewModel()
        {
            alreadyExists = false;
        }
    }
}