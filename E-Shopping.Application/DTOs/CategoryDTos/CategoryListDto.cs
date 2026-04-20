using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shopping.Application.DTOs.CategoryDTos
{
    public class CategoryListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        public string Description { get; set; }
        public int ProductCount { get; set; }
    }
}