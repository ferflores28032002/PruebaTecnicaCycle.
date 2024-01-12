using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaTecnicaCycle.Domain
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [Column(TypeName = "numeric(10, 2)")]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        // Clave foránea para la relación con Category
        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(Int32.MaxValue)]
        public string Image { get; set; } 

        [Required]
        public bool Status { get; set; } // bit en SQL se mapea a bool en C#
    }
}
