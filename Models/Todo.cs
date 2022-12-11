using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace efproyecto.Models
{
    public class Todo
    {
        // [Key]
        public Guid TodoId { get; set; }

        // [ForeignKey("CategoriaId")]
        public Guid CategoryId { get; set; }

        // [Required]
        // [MaxLength(18)]
        public string Title { get; set; }

        // [Required]
        // [MaxLength(180)]
        public string Description { get; set; }
        public Priority TodoPriority { get; set; }
        public Weight Weight { get; set; }
        public DateTime CreationDate { get; set; }

        public virtual Category Category { get; set; }

        // [NotMapped]
        public string Resumen { get; set; }
    }
}

public enum Priority
{
    Low,
    Medium,
    High
}
public enum Weight
{
    Low,
    Medium,
    High
}