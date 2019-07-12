using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryManagementSystem.Models.Library;

namespace LibraryManagementSystem.Tests.Models.Library
{
    /// <summary>
    /// Description résumée pour ClientLibraryDalTest
    /// </summary>
    [TestClass]
    public class ClientLibraryDalTest
    {
        private ILibraryDal dal;

        public ClientLibraryDalTest()
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
        public void AddNewClent_test()
        {
            dal.AddNewClient(ClientInit("Mike", "Smith"));

            List<Client> clients = dal.ClientsList();

            Assert.AreEqual(clients.Count, 1);
        }

        [TestMethod]
        public void DeleteClient_test()
        {
            dal.AddNewClient(ClientInit("Mike", "Smith"));
            dal.AddNewClient(ClientInit("John", "Smith"));

            List<Client> clients = dal.ClientsList();

            Assert.AreEqual(2, dal.ClientsList().Count);
            dal.DeleteClient(clients[1].Id);
            Assert.AreEqual(1, dal.ClientsList().Count);
            dal.DeleteClient(clients[0]);
            Assert.AreEqual(0, dal.ClientsList().Count);
        }

        [TestMethod]
        public void GetClientById_test()
        {
            Client client = ClientInit("Mike", "Smith");
            dal.AddNewClient(client);
            int id = client.Id;
            Assert.AreEqual("Mike", dal.GetClientById(id).FirstName);
        }


        [TestMethod]
        public void GetClientsOfCategory_test()
        {
            ClientCategory studentCategory = new ClientCategory()
            {
                ClientCategoryName = "Student",
                LoanDuration = 15,
                MaxLoans = 1
            };
            ClientCategory teacherCategory = new ClientCategory()
            {
                ClientCategoryName = "Teacher",
                LoanDuration = 30,
                MaxLoans = 2
            };

            Client client1 = ClientInit("Will", "Smith", studentCategory);
            Client client2 = ClientInit("John", "Brown", teacherCategory);
            Client client3 = ClientInit("Steave", "Davis", teacherCategory);

            dal.AddNewClient(client1);
            dal.AddNewClient(client2);
            dal.AddNewClient(client3);

            List<Client> clients = dal.GetClientsOfCategory(teacherCategory);

            Assert.AreEqual(clients.Count, 2);

        }


        /// <summary>
        /// This method is used to create a new instance of a client the quick way for testing
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
        private Client ClientInit(string firstName, string lastName)
        {
            return new Client()
            {
                FirstName = firstName,
                LastName = lastName,
                CIN = firstName + "123445",
                Email = firstName + "@gamil.com"
            };
        }

        private Client ClientInit(string firstName, string lastName, ClientCategory category)
        {
            Client client = ClientInit(firstName, lastName);
            client.Category = category;
            return client;
        }
    }
}
