using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SalesOne.Common.Entities
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "The field {0} must contain less than {1} characteres.")]
        public string Name { get; set; }
    }
}
