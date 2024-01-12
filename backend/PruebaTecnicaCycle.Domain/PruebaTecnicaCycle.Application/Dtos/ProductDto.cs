using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;


namespace PruebaTecnicaCycle.Domain.Dtos
{
    public class ProductDto
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public Guid CategoryId { get; set; }

        public string Description { get; set; }

        public IFormFile Image { get; set; } 

        public bool Status { get; set; }
    }
}
