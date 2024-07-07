using Microsoft.AspNetCore.Identity;

namespace finapp_backend.Models
{
    public class AppUsers : IdentityUser
    {
        public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
    }
}