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
        void AddNewBook(string isbn, string title, int quantity, bool archived);
        void DeleteBook(int bookId);
        void UpdateBook(Book book, Book newBook);
        Book GetBookById(int bookId);
        Book GetBookByISBN(string bookISBN);
        Book GetBookByTitle(string bookTitle);
        List<Book> BookList();

        // Clients
        void AddNewClient(Client client);
        void DeleteClient(int clientId);
        void DeleteClient(Client client);
        void UpdateClient(Client client, Client newClient);
        Client GetClientById(int clientId);
        List<Client> ClientsList();
        List<Client> GetClientsOfCategory(ClientCategory category);

        // Client Categories
        void AddNewClientCategory(ClientCategory clientCategory);
        void DeleteClientCategory(int clientCategoryId);
        void UpdateClientCategory(ClientCategory clientCategory, ClientCategory newClientCategory);
        ClientCategory GetClientCategory(int categoryId);
        List<ClientCategory> GetClientCategories();

        // Loans
        void CreateLoan(Client client, Book book, int duration);
        void AddLoan(ClientBook loan);
        void DeleteLoan(int bookId, int clientId);
        void UpdateLoan(ClientBook loan, ClientBook newLoan);
        List<ClientBook> LoansList();
    }
}
