using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using OneSales.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneSales.Web.Helpers
{
    public class ComboHelper : IComboHelper
    {
        private readonly DataContext _context;
        public ComboHelper(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetComboCategories()
        {
            var lstCategories = _context.Categories.Select(t => new SelectListItem()
            {
                Text = t.Name,
                Value = $"{t.Id}"
            })
                .OrderBy(t => t.Text)
                .ToList();
            lstCategories.Insert(0, new SelectListItem
            {
                Text = "[Select a category...]",
                Value = "0"
            });
            return lstCategories;
        }
    }
}
