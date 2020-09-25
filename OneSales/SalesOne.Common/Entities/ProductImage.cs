using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SalesOne.Common.Entities
{
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Image")]
        public Guid ImageId { get; set; }
        [Display(Name = "Image")]
        public string ImageFullPath => ImageId == Guid.Empty
            ? $"https://localhost:44311/images/noimage.png"
            : $"https://onsale.blob.core.windows.net/products/{ImageId}";
    }
}
