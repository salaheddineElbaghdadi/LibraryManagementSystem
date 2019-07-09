﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSystem.Models.Library
{
    [Table("Loans")]
    public class ClientBook
    {
        [Key]
        public int ClientId { get; set; }
        [Key]
        public int BookId { get; set; }
        /// <summary>
        /// The date in witch the book was borrowed
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// The duration that the client is allowed to have the loan
        /// Deadline
        /// </summary>
        public DateTime LoanDuration { get; set; }
        /// <summary>
        /// The date the client gave back the book
        /// This date may be different than the date that he is not supposed to exceede (deadline) 
        /// </summary>
        public DateTime EndDate { get; set; }

        public virtual Client Client { get; set; }
        public virtual Book Books { get; set; }


        public ClientBook()
        {
            // Setting up the startDate to the moment the ClientBook (Loan) it was created
            StartDate = DateTime.Now;
        }
    }
}