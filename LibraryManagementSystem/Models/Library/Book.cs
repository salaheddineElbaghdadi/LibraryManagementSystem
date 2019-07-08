using System;
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
        public string ISBN { get; set; }
        /// <summary>
        /// Title of the book
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Total Quantity of the book
        /// </summary>
        public int TotalQuantity { get; set; }

        /// <summary>
        /// Clients borrowed the book
        /// </summary>
        public virtual IEnumerable<Client> Clients { get; set; }
    }
}