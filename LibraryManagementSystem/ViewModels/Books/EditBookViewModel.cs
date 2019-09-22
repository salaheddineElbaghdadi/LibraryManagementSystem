using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibraryManagementSystem.Models.Library;

namespace LibraryManagementSystem.ViewModels.Books
{
    public class EditBookViewModel
    {
        public Book book
        {
            get
            {
                return _book;
            }
            set
            {
                //oldISBN = value.ISBN;
                //bookId = value.Id;
                _book = value;
            }
        }
        public int bookId { get; set; }
        public string oldISBN { get; set; }
        public bool alreadyExists { get; set; }
        private Book _book;

        public EditBookViewModel()
        {
            alreadyExists = false;
        }
    }
}