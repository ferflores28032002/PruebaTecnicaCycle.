using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaCycle.Application.Dtos
{
    public class ProductListDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }

        public Guid CategoryId { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public bool Status { get; set; }
    }
}
