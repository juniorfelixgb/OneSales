using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SalesOne.Common.Entities
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50, ErrorMessage = "The field {0} must contain less than {1} characteres.")]
        [Required]
        public string Name { get; set; }
        public ICollection<City> Cities { get; set; }
        [DisplayName("Cities Number")]
        public int CitiesNumber => Cities == null ? 0 : Cities.Count;
        [JsonIgnore]
        [NotMapped]
        public int IdCountry { get; set; }
    }
}
