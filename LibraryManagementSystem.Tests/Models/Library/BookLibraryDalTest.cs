using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryManagementSystem.Models.Library;
using System.Data.Entity;

namespace LibraryManagementSystem.Tests.Models.Library
{
    /// <summary>
    /// Description résumée pour BookLibraryDalTest
    /// </summary>
    [TestClass]
    public class BookLibraryDalTest
    {
        private ILibraryDal dal;

        public BookLibraryDalTest()
        {
            //
            // TODO: ajoutez ici la logique du constructeur
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Obtient ou définit le contexte de test qui fournit
        ///des informations sur la série de tests active, ainsi que ses fonctionnalités.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Attributs de tests supplémentaires
        //
        // Vous pouvez utiliser les attributs supplémentaires suivants lorsque vous écrivez vos tests :
        //
        // Utilisez ClassInitialize pour exécuter du code avant d'exécuter le premier test de la classe
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Utilisez ClassCleanup pour exécuter du code une fois que tous les tests d'une classe ont été exécutés
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Utilisez TestInitialize pour exécuter du code avant d'exécuter chaque test 
        [TestInitialize()]
        public void MyTestInitialize()
        {
            dal = LibraryTestConfig.TestInitializer();
        }
        //
        // Utilisez TestCleanup pour exécuter du code après que chaque test a été exécuté
        [TestCleanup()]
        public void MyTestCleanup()
        {
            dal.Dispose();
        }
        //
        #endregion


        [TestMethod]
        public void AddNewBook_Test()
        {
            dal.AddNewBook("1234567", "Book title", 20);
            List<Book> books = dal.BookList();

            Assert.IsNotNull(books);
            Assert.AreEqual(1, books.Count);
            Assert.AreEqual("1234567", books[0].ISBN);
            Assert.AreEqual("Book title", books[0].Title);
            Assert.AreEqual(20, books[0].TotalQuantity);
        }

        [TestMethod]
        public void GetBookById_Test()
        {
            dal.AddNewBook("1234567", "Book title", 20);
            List<Book> books = dal.BookList();
            Book book = dal.GetBookById(books[0].Id);

            Assert.IsNotNull(book);
            Assert.AreEqual(books[0].ISBN, "1234567");
        }

        [TestMethod]
        public void GetBookByISBN_Test()
        {
            dal.AddNewBook("1234567", "Book title", 20);
            List<Book> books = dal.BookList();
            Book book = dal.GetBookByISBN(books[0].ISBN);

            Assert.IsNotNull(book);
            Assert.AreEqual(books[0].Id, book.Id);
        }
        
        [TestMethod]
        public void GetBookByTitle_Test()
        {
            dal.AddNewBook("1234567", "Book title", 20);
            List<Book> books = dal.BookList();
            Book book = dal.GetBookByTitle(books[0].Title);

            Assert.IsNotNull(book);
            Assert.AreEqual(books[0].ISBN, "1234567");
        }

        [TestMethod]
        public void DeleteBook_Test()
        {
            dal.AddNewBook("1234567", "Book title", 20);
            int id = dal.GetBookByTitle("Book title").Id;
            dal.DeleteBook(id);
            List<Book> books = dal.BookList();

            Assert.AreEqual(books.Count, 0);
        }
    }
}
