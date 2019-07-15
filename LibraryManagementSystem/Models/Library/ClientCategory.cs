using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models.Library
{
    public class ClientCategory
    {
        public int Id { get; set; }
        public string ClientCategoryName { get; set; }
        public int LoanDuration { get; set; }
        public int MaxLoans { get; set; }

    }
}