using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models.Library
{
    public class ClientCategory
    {
        public int ClientCategoryId { get; set; }
        public string ClientCategoryName { get; set; }
        public int LoanDuration { get; set; }
        public int MaxLoans { get; set; }

        //public virtual ICollection<Client> Clients { get; set; }
        //public virtual ICollection<PropertyBag> Properties { get; set; }
    }
}