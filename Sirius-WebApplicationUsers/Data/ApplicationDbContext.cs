using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sirius_WebApplicationUsers.Models;

namespace Sirius_WebApplicationUsers.Data
{
    public class ApplicationDbContext : IdentityDbContext<PortalUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
