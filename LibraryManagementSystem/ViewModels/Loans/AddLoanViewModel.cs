using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibraryManagementSystem.Models.Library;

namespace LibraryManagementSystem.ViewModels.Loans
{
    public class AddLoanViewModel
    {
        public int clientId { get; set; }
        public ClientBook loan { get; set; }
    }
}