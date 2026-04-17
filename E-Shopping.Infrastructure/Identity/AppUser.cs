using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Shopping.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace E_Shopping.Infrastructure.Identity
{
    public class AppUser : IdentityUser<string>
    {
        public Address Addresses { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}