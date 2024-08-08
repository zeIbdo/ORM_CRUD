using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_CRUD.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public string Name { get; set; } = null!;
        public ICollection<Category> Categories { get; set; }=new List<Category>();
        public override string ToString()
        {
            return $"{Id} {Name}";
        }
    }
}
