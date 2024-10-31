using Microsoft.EntityFrameworkCore;
using Sirius_WebApplication.Models;

namespace Sirius_WebApplication.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
    }
}
