using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Npgsql;

namespace LibraryManagementSystem.Models.Library
{
    /*
    class NpgSqlConfiguration : DbConfiguration
    {
        public NpgSqlConfiguration()
        {
            var name = "Npgsql";

            SetProviderFactory(providerInvariantName: name,
            providerFactory: NpgsqlFactory.Instance);

            SetProviderServices(providerInvariantName: name,
            provider: NpgsqlServices.Instance);

            SetDefaultConnectionFactory(connectionFactory: new NpgsqlConnectionFactory());
        }
    }*/

    public class LibraryContext : DbContext
    {
        public LibraryContext() : base("LibraryContext")
        {
            Database.SetInitializer<LibraryContext>(new DropCreateDatabaseAlways<LibraryContext>());
            //Database.SetInitializer<LibraryContext>(new DropCreateDatabaseIfModelChanges<LibraryContext>());
        }
        

        public DbSet<Book> Books { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientBook> ClientBooks { get; set; }
        public DbSet<ClientCategory> ClientCategories { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientBook>()
                .HasKey(c => new { c.ClientId, c.BookId });

            modelBuilder.Entity<Client>()
                .HasMany(c => c.ClientBooks)
                .WithOptional()
                .HasForeignKey(c => c.ClientId);

            modelBuilder.Entity<Book>()
                .HasMany(b => b.ClientBooks)
                .WithOptional()
                .HasForeignKey(l => l.BookId);


            base.OnModelCreating(modelBuilder);
        }
    }
}