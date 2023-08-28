using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BookStoreAdmin.Entity
{
    public class AdminContext : DbContext
    {
        public AdminContext(DbContextOptions<AdminContext> options) : base(options)
        {

        }
        public DbSet<AdminEntity> Admin { get; set; }
    }
}
