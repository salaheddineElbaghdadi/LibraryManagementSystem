using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Models.Library
{
    public interface ILibraryDal : IDisposable
    {

        // Books
        void AddNewBook(string isbn, string title, int quantity);
        void DeleteBook(int bookId);
        Book GetBookById(int bookId);
        Book GetBookByISBN(string bookISBN);
        Book GetBookByTitle(string bookTitle);
        List<Book> BookList();

        // Clients
        void AddNewClient<ClientNature>(string firstName, string lastName, string CIN, string email);
        void DeleteClient(int clientId);
    }
}
