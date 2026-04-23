using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shopping.Application.DTOs.CategoryDTos
{
    public class CategoryCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}