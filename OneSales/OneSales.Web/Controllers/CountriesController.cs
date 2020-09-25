using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OneSales.Web.Data;
using SalesOne.Common.Entities;

namespace OneSales.Web.Controllers
{
    public class CountriesController : Controller
    {
        private readonly DataContext _context;

        public CountriesController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Countries
                                      .Include(c => c.Departments)
                                      .ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Countries
                .Include(c => c.Departments)
                .ThenInclude(d => d.Cities)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Country country)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(country);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
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
                catch(Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(country);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Country country)
        {
            if (id != country.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(country);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
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
                return RedirectToAction(nameof(Index));
            }
            return View(country);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Countries
                .Include(c => c.Departments)
                .ThenInclude(d => d.Cities)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (country == null)
            {
                return NotFound();
            }
            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> AddDepartment(int? Id)
        {
            var country = await _context.Countries.FindAsync(Id);

            var model = new Department() { IdCountry = country.Id };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDepartment(Department department)
        {
            if (ModelState.IsValid)
            {
                var country = await _context.Countries
                    .Include(c => c.Departments)
                    .FirstOrDefaultAsync(c => c.Id == department.IdCountry);

                try
                {
                    department.Id = 0;
                    country.Departments.Add(department);
                    _context.Update(country);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", "Countries", new { Id = country.Id});
                }
                catch (DbUpdateException UpdateException)
                {
                    if (UpdateException.Data.Contains("duplicate"))
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
            return View(department);
        }

        [HttpGet]
        public async Task<IActionResult> EditDepartment(int? Id)
        {
            var department = await _context.Departments.FindAsync(Id);
            var country = await _context.Countries
                                .FirstOrDefaultAsync(c => c.Departments
                                .FirstOrDefault(d => d.Id == department.Id) != null);
            department.IdCountry = country.Id;
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDepartment(Department department)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Details", "Countries", new { Id = department.IdCountry });
                }
                catch (DbUpdateException UpdateException)
                {
                    if (UpdateException.Data.Contains("duplicate"))
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
            return View(department);
        }

        public async Task<IActionResult> DetailsDepartment(int? Id)
        {
            var department = await _context.Departments
                                           .Include(d => d.Cities)
                                           .FirstOrDefaultAsync(c => c.Id == Id);
            var country = await _context.Countries.FirstOrDefaultAsync(c => c.Departments
                                                  .FirstOrDefault(d => d.Id == department.Id) != null);
            department.IdCountry = country.Id;
            return View(department);
        }

        public async Task<IActionResult> DeleteDepartment(int? Id)
        {
            var department = await _context.Departments
                                           .Include(d => d.Cities)
                                           .FirstOrDefaultAsync(m => m.Id == Id);
            var country = await _context.Countries
                                        .FirstOrDefaultAsync(c => c.Departments
                                        .FirstOrDefault(d => d.Id == department.Id) != null);
            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Countries", new { Id = country.Id });
        }

        public async Task<IActionResult> AddCity(int? Id)
        {
            var department = await _context.Departments.FindAsync(Id);
            var city = new City() { IdDepartment = department.Id };
            return View(city);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCity(City city)
        {
            if (ModelState.IsValid)
            {
                var department = await _context.Departments
                                               .Include(d => d.Cities)
                                               .FirstOrDefaultAsync(c => c.Id == city.IdDepartment);
                try
                {
                    city.Id = 0;
                    department.Cities.Add(city);
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("DetailsDepartment", "Countries", new { Id = department.Id });
                }
                catch (DbUpdateException UpdateException)
                {
                    if (UpdateException.Data.Contains("duplicate"))
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
            return View();
        }

        public async Task<IActionResult> EditCity(int? Id)
        {
            var city = await _context.Cities.FindAsync(Id);
            var department = await _context.Departments.FirstOrDefaultAsync(d => d.Cities
                                                       .FirstOrDefault(c => c.Id == city.Id) != null);
            city.IdDepartment = department.Id;
            return View(city);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCity(City city)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(city);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("DetailsDepartment", new { Id = city.IdDepartment});
                }
                catch (DbUpdateException UpdateException)
                {
                    if (UpdateException.Data.Contains("duplicate"))
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
            return View(city);
        }

        public async Task<IActionResult> DeleteCity(int? Id)
        {
            var city = await _context.Cities
                                    .FirstOrDefaultAsync(m => m.Id == Id);
            var department = await _context.Departments.FirstOrDefaultAsync(d => d.Cities
                                                       .FirstOrDefault(c => c.Id == city.Id) != null);
            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();
            return RedirectToAction("DetailsDepartment", new { Id = department.Id });
        }
    }
}
