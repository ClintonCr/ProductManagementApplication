using System;
using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Contract.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }

        [Range(double.Epsilon, 999999999.99, ErrorMessage = "Price must be a positive non-zero value")]
        public decimal Price { get; set; }

        [Range(double.Epsilon, 999999999.99, ErrorMessage = "Delivery Price must be a positive non-zero value")]
        public decimal DeliveryPrice { get; set; }
    }
}
