using Microsoft.AspNetCore.Mvc;
using VStudio.Models;

namespace VStudio.Controllers
{
    public class SupplierController : Controller
    {
        private NorthwindDBContext _context { get; set; }

        public SupplierController(NorthwindDBContext context)
        {
            _context = context;
        }

        public IActionResult Index(string searchStr)
        {
            if (string.IsNullOrEmpty(searchStr))
            {
                return View(_context.Suppliers.ToList());
            }
            return View(_context.Suppliers.Where(sp => sp.CompanyName.ToLower().Contains(searchStr.ToLower())).ToList());
        }

        [HttpPost]
        public string Index(string searchStr, bool notUsed)
        {
            return searchStr;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Supplier supplier)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _context.Suppliers.Add(supplier);
            _context.SaveChangesAsync();

            return RedirectToAction("Index", "Supplier");
        }

        public IActionResult Delete(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var supDb = _context.Suppliers.FirstOrDefault(s => s.SupplierId == id);

            if (supDb is null) return NotFound();

            _context.Suppliers.Remove(supDb);
            _context.SaveChanges();

            return RedirectToAction("index", "supplier");
        }

        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var supDb = _context.Suppliers.FirstOrDefault(s => s.SupplierId == id);

            if (supDb is null) return NotFound();

            return View(supDb);
        }

        [HttpPost]
        public IActionResult Edit(int? id, Supplier supplier)
        {
            if (!id.HasValue) return BadRequest();

            if (!ModelState.IsValid) return View();

            var supDb = _context.Suppliers.FirstOrDefault(s => s.SupplierId == id);

            if (supDb is null) return NotFound();

            supDb.CompanyName = supplier.CompanyName;

            _context.Suppliers.Update(supDb);
            _context.SaveChanges();

            return RedirectToAction("Index", "Supplier");
        }
    }
}
