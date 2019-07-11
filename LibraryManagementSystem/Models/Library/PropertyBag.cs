using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSystem.Models.Library
{
    public class PropertyBag
    {
        [ForeignKey("Client")]
        public int Id { get; set; }

        public Dictionary<string, object> Properties { get; set; }

        public int ClientCategoryId { get; set; }
        public virtual ClientCategory ClientCategory { get; set; }

        public virtual Client Client { get; set; }
    }
}