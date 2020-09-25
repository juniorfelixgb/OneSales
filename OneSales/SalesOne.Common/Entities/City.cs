using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SalesOne.Common.Entities
{
    public class City
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = "The field {0} must contain less than {1} characteres.")]
        [Required]
        public string Name { get; set; }
        [JsonIgnore]
        [NotMapped]
        public int IdDepartment { get; set; }
    }
}
