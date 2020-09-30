using OneSales.Web.Models;
using SalesOne.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneSales.Web.Helpers
{
    public interface IConverterHelper
    {
        Category ToCategory(CategoryViewModel viewModel, Guid imageId, bool isNew);
        CategoryViewModel ToCategoryViewModel(Category model);
        Task<Product> ToProductAsync(ProductViewModel viewModel, bool isNew);
        ProductViewModel ToProductViewModel(Product product);
    }
}
