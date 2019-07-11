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
        void AddNewBook(Book book);
        void AddNewBook(string isbn, string title, int quantity);
        void DeleteBook(int bookId);
        Book GetBookById(int bookId);
        Book GetBookByISBN(string bookISBN);
        Book GetBookByTitle(string bookTitle);
        List<Book> BookList();

        // Clients
        void AddNewClient(Client client);
        void DeleteClient(int clientId);
        Client GetClientById(int clientId);
        List<Client> ClientsList();

        // Client Categories
        void AddNewClientCategory(ClientCategory clientCategory);
        ClientCategory GetClientCategory(int categoryId);
        void DeleteClientCategory(int clientCategoryId);

        // Loans
        void AddLoan(Client client, Book book, int duration);
        List<ClientBook> LoansList();
    }
}
