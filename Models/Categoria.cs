using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace efproyecto.Models
{
    public class Category
    {
        // [Key]
        public Guid CategoryId { get; set; }

        // [Required]
        // [MaxLength(18)]
        public string Title { get; set; }

        // [Required]
        // [MaxLength(180)]
        public string Description { get; set; }

        [JsonIgnore]
        public virtual ICollection<Todo> Todos { get; set; }
    }
}
