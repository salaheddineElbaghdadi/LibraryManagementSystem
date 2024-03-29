﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibraryManagementSystem.Models.Library;

namespace LibraryManagementSystem.ViewModels.Loans
{
    public class AddLoanViewModel
    {
        public ClientBook loan { get; set; }
        public string ISBN { get; set; }
        public bool bookFound { get; set; }

        public AddLoanViewModel()
        {
            bookFound = true;
        }
    }
}