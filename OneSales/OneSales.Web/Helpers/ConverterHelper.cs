using OneSales.Web.Data;
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
        private readonly DataContext _context;
        private readonly IComboHelper _comboHelper;
        public ConverterHelper(DataContext context,
                               IComboHelper comboHelper)
        {
            _context = context;
            _comboHelper = comboHelper;
        }
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

        public async Task<Product> ToProductAsync(ProductViewModel viewModel, bool isNew)
        {
            return new Product()
            {
                Category = await _context.Categories.FindAsync(viewModel.CategoryId),
                Description = viewModel.Description,
                Id = isNew ? 0 : viewModel.Id,
                IsActive = viewModel.IsActive,
                IsStarred = viewModel.IsStarred,
                Name = viewModel.Name,
                Price = viewModel.Price,
                ProductImages = viewModel.ProductImages
            };
        }

        public ProductViewModel ToProductViewModel(Product product)
        {
            return new ProductViewModel()
            {
                Categories = _comboHelper.GetComboCategories(),
                Category = product.Category,
                CategoryId = product.Category.Id,
                Description = product.Description,
                Id = product.Id,
                IsActive = product.IsActive,
                IsStarred = product.IsStarred,
                Name = product.Name,
                Price = product.Price,
                ProductImages = product.ProductImages
            };
        }
    }
}
