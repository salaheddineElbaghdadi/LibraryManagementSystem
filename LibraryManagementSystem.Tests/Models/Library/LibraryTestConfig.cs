using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.Models.Library;
using System.Data.Entity;

namespace LibraryManagementSystem.Tests.Models.Library
{
    public static class LibraryTestConfig
    {
        public static LibraryDal TestInitializer()
        {
            IDatabaseInitializer<LibraryContext> init = new DropCreateDatabaseAlways<LibraryContext>();
            Database.SetInitializer(init);
            init.InitializeDatabase(new LibraryContext());
            return new LibraryDal();
        }
    }
}
