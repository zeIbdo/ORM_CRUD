using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_CRUD.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public string Name { get; set; } = null!;
        public int CategoryId { get; set; }
        [Required]
        [Range(0, 1000)]
        public decimal Price { get; set; }
        public Category category { get; set; } = null!;
        public override string ToString()
        {
            return $"{Id} {Name} {Price} {category.Name}";
        }
    }
}
