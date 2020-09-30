using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OneSales.Web.Data;
using OneSales.Web.Helpers;
using OneSales.Web.Models;
using SalesOne.Common.Entities;

namespace OneSales.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly DataContext _context;
        private readonly IBlobHelper _blobHelper;
        private readonly IComboHelper _comboHelper;
        private readonly IConverterHelper _converterHelper;
        public ProductsController(DataContext context,
                                  IBlobHelper blobHelper,
                                  IComboHelper comboHelper,
                                  IConverterHelper converterHelper)
        {
            _context = context;
            _blobHelper = blobHelper;
            _comboHelper = comboHelper;
            _converterHelper = converterHelper;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products
                                      .Include(p => p.Category)
                                      .Include(p => p.ProductImages)
                                      .ToListAsync());
        }

        public IActionResult Create()
        {
            ProductViewModel viewModel = new ProductViewModel
            {
                Categories = _comboHelper.GetComboCategories(),
                IsActive = true
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var product = await _converterHelper.ToProductAsync(viewModel, true);
                    if (viewModel.ImageFile != null)
                    {
                        Guid imageId = await _blobHelper.UploadBlobAsync(viewModel.ImageFile, "products");
                        product.ProductImages = new List<ProductImage> { new ProductImage { ImageId = imageId } };
                    }
                    _context.Add(product);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateException UpdateException)
                {
                    if (UpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, UpdateException.InnerException.Message);
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            viewModel.Categories = _comboHelper.GetComboCategories();
            return View(viewModel);
        }
    }
}
