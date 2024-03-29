﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models.Library
{

    public class Client
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string CIN { get; set; }
        public string Email { get; set; }

        /// <summary>
        /// Books borrowed by this client
        /// </summary>
        public virtual List<ClientBook> ClientBooks { get; set; }
        /// <summary>
        /// The Category of the client
        /// </summary>
        [Required]
        public virtual ClientCategory Category { get; set; }

        public Client()
        {
            ClientBooks = new List<ClientBook>();
        }
    }
}