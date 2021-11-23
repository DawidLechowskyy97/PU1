using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Database : DbContext
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<AuthorRate> AuthorsRate { get; set; }
        public DbSet<BookRate> BooksRate { get; set; }

        public Database()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source=E:\\studia\\PU_Lab1CQRS_Dawid_Lechowski_MIP10\\PULab1CQRS\\Model");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Rate>()
                .HasDiscriminator(x => x.Type)
                .HasValue<AuthorRate>(RateType.AuthorRate)
                .HasValue<BookRate>(RateType.BookRate);
        }
    }
}
