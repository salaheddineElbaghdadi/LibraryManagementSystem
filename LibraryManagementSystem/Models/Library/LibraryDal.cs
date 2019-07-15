using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManagementSystem.Models.Library
{
    public class LibraryDal : ILibraryDal
    {
        private LibraryContext LibraryDb;
        
        /// <summary>
        /// Constructor
        /// Making a new instance of the LibraryContext class to access the library Database
        /// </summary>
        public LibraryDal()
        {
            LibraryDb = new LibraryContext();
        }

        #region Book

        /// <summary>
        /// Add a new book into the databse
        /// </summary>
        /// <param name="book">The instance of the book to add</param>
        public void AddNewBook(Book book)
        {
            LibraryDb.Books.Add(book);
        }

        /// <summary>
        /// Add a new book into the database
        /// </summary>
        /// <param name="isbn">The ISBN of the book</param>
        /// <param name="title">The title of the book</param>
        /// <param name="quantity">The quantity of the book that will be availbale in the library</param>
        public void AddNewBook(string isbn, string title, int quantity)
        {
            Book newBook = new Book() { ISBN = isbn, Title = title, TotalQuantity = quantity };
            LibraryDb.Books.Add(newBook);
            LibraryDb.SaveChanges();
        }

        /// <summary>
        /// Delete a book from the database
        /// </summary>
        /// <param name="bookId">BookId</param>
        public void DeleteBook(int bookId)
        {
            Book book = LibraryDb.Books.First(b => b.Id == bookId);
            LibraryDb.Books.Remove(book);
            LibraryDb.SaveChanges();
        }
        
        /// <summary>
        /// Returns book instance by id
        /// </summary>
        /// <param name="bookId">Book id</param>
        /// <returns></returns>
        public Book GetBookById(int bookId)
        {
            Book book = LibraryDb.Books.FirstOrDefault(b => b.Id == bookId);
            if (book != null)
                return book;
            return null;
        }

        /// <summary>
        /// Return book instance by isbn
        /// </summary>
        /// <param name="bookISBN">Bood ISBN</param>
        /// <returns></returns>
        public Book GetBookByISBN(string bookISBN)
        {
            return LibraryDb.Books.FirstOrDefault(b => b.ISBN == bookISBN);
        }

        /// <summary>
        /// Return book instance by title
        /// </summary>
        /// <param name="bookTitle">book title</param>
        /// <returns></returns>
        public Book GetBookByTitle(string bookTitle)
        {
            return LibraryDb.Books.FirstOrDefault(b => b.Title == bookTitle);
        }

        /// <summary>
        /// Return List of all books in the database
        /// </summary>
        /// <returns></returns>
        public List<Book> BookList()
        {
            return LibraryDb.Books.ToList();
        }

        #endregion  Book

        #region Client

        /// <summary>
        /// Add new client to the library
        /// </summary>
        /// <param name="client"></param>
        public void AddNewClient(Client client)
        {
            LibraryDb.Clients.Add(client);
            LibraryDb.SaveChanges();
        }


        /// <summary>
        /// Remove client by id
        /// </summary>
        /// <param name="clientId">client id</param>
        public void DeleteClient(int clientId)
        {
            Client client = GetClientById(clientId);
            LibraryDb.Clients.Remove(client);
            LibraryDb.SaveChanges();
        }

        /// <summary>
        /// Remove client by reference
        /// </summary>
        /// <param name="client">client reference</param>
        public void DeleteClient(Client client)
        {
            LibraryDb.Clients.Remove(client);
            LibraryDb.SaveChanges();
        }

        /// <summary>
        /// Get client using id
        /// </summary>
        /// <param name="clientId">client id</param>
        /// <returns></returns>
        public Client GetClientById(int clientId)
        {
            return LibraryDb.Clients.First(c => c.Id == clientId);
        }

        /// <summary>
        /// return clients list
        /// </summary>
        /// <returns></returns>
        public List<Client> ClientsList()
        {
            return LibraryDb.Clients.ToList();
        }

        public List<Client> GetClientsOfCategory(ClientCategory category)
        {
            List<Client> clients = new List<Client>();
            foreach (Client client in LibraryDb.Clients.ToList())
            {
                if (client.Category == category)
                    clients.Add(client);
            }
            return clients;
        }

        #endregion Client

        #region ClientCategory

        public void AddNewClientCategory(ClientCategory category)
        {
            LibraryDb.ClientCategories.Add(category);
            LibraryDb.SaveChanges();
        }

        public ClientCategory GetClientCategory(int categoryId)
        {
            return LibraryDb.ClientCategories.First(c => c.Id == categoryId);
        }

        public void DeleteClientCategory(int categoryId)
        {
            ClientCategory clientCategory = GetClientCategory(categoryId);
            LibraryDb.ClientCategories.Remove(clientCategory);
            LibraryDb.SaveChanges();
        }

        public List<ClientCategory> GetClientCategories()
        {
            return LibraryDb.ClientCategories.ToList();
        }

        #endregion ClientCategory

        #region ClientBook

        public List<ClientBook> LoansList()
        {
            return LibraryDb.ClientBooks.ToList();
        }


        public void AddLoan(Client client, Book book, int duration)
        {
            ClientBook loan = new ClientBook
            {
                ClientId = client.Id,
                BookId = book.Id,
                LoanDuration = duration,
                //Client = client,
                //Book = book,
            };
            client.ClientBooks.Add(loan);
            book.ClientBooks.Add(loan);

            LibraryDb.ClientBooks.Add(loan);
            LibraryDb.SaveChanges();
        }
        #endregion ClientBook

        #region IDisposable Support
        private bool disposedValue = false; // Pour détecter les appels redondants

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: supprimer l'état managé (objets managés).
                }

                // TODO: libérer les ressources non managées (objets non managés) et remplacer un finaliseur ci-dessous.
                // TODO: définir les champs de grande taille avec la valeur Null.

                disposedValue = true;
            }
        }

        // TODO: remplacer un finaliseur seulement si la fonction Dispose(bool disposing) ci-dessus a du code pour libérer les ressources non managées.
        // ~LibraryDal() {
        //   // Ne modifiez pas ce code. Placez le code de nettoyage dans Dispose(bool disposing) ci-dessus.
        //   Dispose(false);
        // }

        // Ce code est ajouté pour implémenter correctement le modèle supprimable.
        public void Dispose()
        {
            // Ne modifiez pas ce code. Placez le code de nettoyage dans Dispose(bool disposing) ci-dessus.
            Dispose(true);
            // TODO: supprimer les marques de commentaire pour la ligne suivante si le finaliseur est remplacé ci-dessus.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}