using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibraryManagementSystem.Models.Library;

namespace LibraryManagementSystem.ViewModels.Loans
{
    public class LoansViewModel
    {
        public List<ClientBook> Loans { get; set; }
        public List<string> BookTitles { get; set; }
        public List<string> ClientFirstNames { get; set; }
        public List<string> ClientLastNames { get; set; }
    }
}