using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Activity2.Models
{
    public class ProductModel
    {
        [DisplayName("Id number")]
        public int Id { get; set; }
        [DisplayName("Product Name")]
        public string Name { get; set; }
        [DisplayName("Cost of customer")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [DisplayName("What you get...")]
        public string Description { get; set; }
    }
}
