using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BookStore.Book.Entity
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> options) : base(options)
        {

        }
        public DbSet<BookEntity> Book { get; set; }
    }
}
