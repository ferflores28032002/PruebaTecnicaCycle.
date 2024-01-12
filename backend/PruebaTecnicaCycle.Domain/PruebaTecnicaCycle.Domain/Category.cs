using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnicaCycle.Domain
{
    public class Category
    {
        public Guid CategoryId { get; set; }

    
        public string CategoryName { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
