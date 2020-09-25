using OneSales.Web.Models;
using SalesOne.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneSales.Web.Helpers
{
    public class ConverterHelper : IConverterHelper
    {
        public Category ToCategory(CategoryViewModel viewModel, Guid imageId, bool isNew)
        {
            return new Category()
            {
                Id = isNew ? 0 : viewModel.Id,
                ImageId = imageId,
                Name = viewModel.Name
            };
        }

        public CategoryViewModel ToCategoryViewModel(Category model)
        {
            return new CategoryViewModel()
            {
                Id = model.Id,
                ImageId = model.ImageId,
                Name = model.Name
            };
        }
    }
}
