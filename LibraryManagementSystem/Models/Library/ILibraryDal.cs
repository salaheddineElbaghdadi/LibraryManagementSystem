﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Models.Library
{
    interface ILibraryDal : IDisposable
    {

        // Books
        void AddNewBook(string isbn, string title, int quantity);
        void DeleteBook(int bookId);

        // Clients
        void AddNewClient<ClientNature>(string firstName, string lastName, string CIN, string email);
        void DeleteClient(int clientId);
    }
}