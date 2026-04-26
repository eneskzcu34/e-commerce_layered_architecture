
using E_Shopping.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace E_Shopping.Infrastructure.Identity
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Addresses { get; set; }
        public Order Order { get; set; }
    }
}