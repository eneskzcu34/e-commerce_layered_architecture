using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shopping.Application.DTOs.CategoryDTos
{
    public class CategoryUpdateDtos
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime UpdateDate { get; set; } = DateTime.Now;
    }
}