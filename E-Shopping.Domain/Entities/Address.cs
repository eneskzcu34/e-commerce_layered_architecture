using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shopping.Domain.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public string Title { get; set; } 
        public string City { get; set; }
        public string District { get; set; }
        public string FullAddress { get; set; }

    }
}