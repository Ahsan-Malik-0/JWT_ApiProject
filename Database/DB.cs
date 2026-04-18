using Microsoft.EntityFrameworkCore;
using WebProjectAPIs.Models;

namespace WebProjectAPIs.Database
{
    public class DB(DbContextOptions<DB> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
    }
}