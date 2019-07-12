using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryManagementSystem.Models.Library;

namespace LibraryManagementSystem.Tests.Models.Library
{
    /// <summary>
    /// Description résumée pour ClientCategoryLibraryDalTest
    /// </summary>
    [TestClass]
    public class ClientCategoryLibraryDalTest
    {
        private ILibraryDal dal;

        public ClientCategoryLibraryDalTest()
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
        public void AddNewClientCategory_test()
        {
            ClientCategory category = CategoryInit("Student", 15, 1);
            dal.AddNewClientCategory(category);

            Assert.AreEqual(dal.GetClientCategories().Count, 1);
        }

        [TestMethod]
        public void GetClientCategory_test()
        {
            ClientCategory category = CategoryInit("Student", 15, 1);
            Client client = new Client()
            {
                FirstName = "Will",
                LastName = "Smith",
                CIN = "BK212121",
                Email = "w.smith@gmail.com",
                Category = category
            };

            ClientCategory newCategory = client.Category;
            Assert.AreEqual(category.ClientCategoryName, newCategory.ClientCategoryName);
            Assert.AreEqual(category.LoanDuration, newCategory.LoanDuration);
            Assert.AreEqual(category.MaxLoans, newCategory.MaxLoans);
        }

        public ClientCategory CategoryInit(string name, int loanDuration, int maxLoans)
        {
            return new ClientCategory()
            {
                ClientCategoryName = name,
                LoanDuration = loanDuration,
                MaxLoans = maxLoans
            };
        }
    }
}
