using System;
using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Contract.Models
{
    public class ProductOption
    {
        public Guid Id { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }
    }
}
