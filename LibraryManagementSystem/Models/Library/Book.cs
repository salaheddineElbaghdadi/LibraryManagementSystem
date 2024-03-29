﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models.Library
{
    public class Book
    {
        /// <summary>
        /// Id != ISBN
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// ISBN : International Standard Book Number
        /// </summary>
        [Required]
        [StringLength(maximumLength: 13, MinimumLength = 10)]
        public string ISBN { get; set; }
        /// <summary>
        /// Title of the book
        /// </summary>
        [Required]
        public string Title { get; set; }
        /// <summary>
        /// Total Quantity of the book
        /// </summary>
        [Required]
        public int TotalQuantity { get; set; }
        /// <summary>
        /// Is the book archived or not
        /// </summary>
        public bool Archived { get; set; }

        /// <summary>
        /// Clients borrowed the book
        /// </summary>
        public virtual List<ClientBook> ClientBooks { get; set; }

        public Book()
        {
            ClientBooks = new List<ClientBook>();
        }
    }
}