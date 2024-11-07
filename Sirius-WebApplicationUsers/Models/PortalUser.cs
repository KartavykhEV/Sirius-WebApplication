using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Sirius_WebApplicationUsers.Models
{
    public class PortalUser : IdentityUser
    {
        public string? FName { get; set; }
        public string? LName { get; set; }
    }

}
