using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace LibraryManagementSystem.Models.Library
{
    public class LibraryContext : DbContext
    {
        public LibraryContext() : base()
        {

        }
        

        public DbSet<Book> Books { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientBook> ClientBooks { get; set; }
        public DbSet<ClientCategory> ClientCategories { get; set; }
        public DbSet<PropertyBag> PropertyBags { get; set; }

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