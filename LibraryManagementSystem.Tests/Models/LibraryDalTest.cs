using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryManagementSystem.Models.Library;
using System.Data.Entity;

namespace LibraryManagementSystem.Tests.Models
{
    /// <summary>
    /// Description résumée pour LibraryDalTest
    /// </summary>
    [TestClass]
    public class LibraryDalTest
    {

        private ILibraryDal dal;

        public LibraryDalTest()
        {
            
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
        public void TestInitialize()
        {
            IDatabaseInitializer<LibraryContext> init = new DropCreateDatabaseAlways<LibraryContext>();
            Database.SetInitializer(init);
            init.InitializeDatabase(new LibraryContext());
            dal = new LibraryDal();
        }

        // Utilisez TestCleanup pour exécuter du code après que chaque test a été exécuté
        [TestCleanup()]
        public void MyTestCleanup()
        {
            dal.Dispose();
        }
        
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

        [TestMethod]
        public void AddNewClent_test()
        {
            Student student = new Student()
            {
                FirstName = "SFN",
                LastName = "SLN",
                CIN = "BK121212",
                CNE = "12345",
                Email = "student@gmail.com"
            };

            Teacher teacher = new Teacher()
            {
                FirstName = "TFN",
                LastName = "TLN",
                CIN = "BK343434",
                Email = "teacher@gmail.com"
            };

            dal.AddNewClient(student);
            dal.AddNewClient(teacher);

            List<Client> clients = dal.ClientsList();

            Assert.AreEqual(clients.Count, 2);
        }

        [TestMethod]
        public void DeleteClient_test()
        {
            Student student = new Student()
            {
                FirstName = "SFN",
                LastName = "SLN",
                CIN = "BK121212",
                CNE = "12345",
                Email = "student@gmail.com"
            };

            dal.AddNewClient(student);
            Assert.AreEqual(1, dal.ClientsList().Count);
            dal.DeleteClient(student.Id);
            Assert.AreEqual(0, dal.ClientsList().Count);
        }

        [TestMethod]
        public void GetClientById_test()
        {
            Student student = new Student()
            {
                FirstName = "SFN",
                LastName = "SLN",
                CIN = "BK121212",
                CNE = "12345",
                Email = "student@gmail.com"
            };

            dal.AddNewClient(student);
            int id = student.Id;
            Assert.AreEqual("SFN", dal.GetClientById(id).FirstName);
        }

        [TestMethod]
        public void IsClientStudent_test()
        {
            Student student = new Student()
            {
                FirstName = "SFN",
                LastName = "SLN",
                CIN = "BK121212",
                CNE = "12345",
                Email = "student@gmail.com"
            };

            Client client = student;
            Assert.IsTrue(dal.IsClientStudent(client));
        }

        [TestMethod]
        public void IsClientTeacher_test()
        {
            Teacher teacher = new Teacher()
            {
                FirstName = "TFN",
                LastName = "TLN",
                CIN = "BK343434",
                Email = "teacher@gmail.com"
            };

            Client client = teacher;
            Assert.IsTrue(dal.IsClientTeacher(client));
        }

        [TestMethod]
        public void ClientsList_test()
        {

        }

        [TestMethod]
        public void GetAllStudents_test()
        {
            Student student = new Student()
            {
                FirstName = "SFN",
                LastName = "SLN",
                CIN = "BK121212",
                CNE = "12345",
                Email = "student@gmail.com"
            };

            Teacher teacher = new Teacher()
            {
                FirstName = "TFN",
                LastName = "TLN",
                CIN = "BK343434",
                Email = "teacher@gmail.com"
            };

            dal.AddNewClient(student);
            dal.AddNewClient(teacher);

            Assert.AreEqual(2, dal.ClientsList().Count);
            List<Student> students = dal.GetAllStudents();
            Assert.AreEqual(1, students.Count);
            Assert.AreEqual(student.Id, students[0].Id);
        }

        [TestMethod]
        public void GetAllTeachers_test()
        {

        }

        [TestMethod]
        public void AddLoan_test()
        {
            Student student = new Student()
            {
                FirstName = "SFN",
                LastName = "SLN",
                CIN = "BK121212",
                CNE = "12345",
                Email = "student@gmail.com"
            };

            Book book = new Book()
            {
                ISBN = "1234567",
                Title = "Book title",
                TotalQuantity = 20
            };

            dal.AddNewClient(student);
            dal.AddNewBook(book);

            TimeSpan duration = new TimeSpan(15, 0, 0, 0);

            // Adding the loan
            dal.AddLoan(student, book, duration);

            // Checking
            Assert.AreEqual(1, dal.LoansList().Count);
            Assert.AreEqual(1, book.Loans.Count);
            Assert.AreEqual(1, student.Loans.Count);
        }
    }
}
