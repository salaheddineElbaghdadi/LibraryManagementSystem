using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibraryManagementSystem.Models.Library;

namespace LibraryManagementSystem.ViewModels.Loans
{
    public class EditLoanViewModel
    {
        public int clientId { get; set; }
        public int bookId { get; set; }
        public ClientBook loan { get; set; }
    }
}