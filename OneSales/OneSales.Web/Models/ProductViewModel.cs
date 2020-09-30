using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using SalesOne.Common.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OneSales.Web.Models
{
    public class ProductViewModel : Product
    {
        [Display(Name="Category")]
        [Range(1, int.MaxValue, ErrorMessage ="You must select a category.")]
        [Required]
        public int CategoryId { get; set; }
        public IFormFile ImageFile { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
