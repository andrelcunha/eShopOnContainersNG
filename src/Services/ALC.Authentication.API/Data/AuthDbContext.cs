using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ALC.Authentication.API.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext( DbContextOptions<AuthDbContext> options) : base(options) { }

    }
}