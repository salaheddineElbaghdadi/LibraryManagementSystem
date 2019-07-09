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
        public DbSet<ClientBook> Loans { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientBook>()
                .HasKey(c => new { c.ClientId, c.BookId });

            modelBuilder.Entity<Client>()
                .HasMany(c => c.Loans)
                .WithOptional()
                .HasForeignKey(c => c.ClientId);

            modelBuilder.Entity<Book>()
                .HasMany(b => b.Loans)
                .WithOptional()
                .HasForeignKey(l => l.BookId);

            base.OnModelCreating(modelBuilder);
        }
    }
}