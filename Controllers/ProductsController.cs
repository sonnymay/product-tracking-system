using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySimpleWebApp.Data;
using MySimpleWebApp.Models;
using System.Text;

namespace MySimpleWebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(
            string type = "In",
            string? searchString = null,
            string? category = null,
            string? sortOrder = null,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            ViewBag.Type = type;
            ViewBag.CurrentSort = sortOrder;
            
            // Handle null RecordType values
            var products = _context.Products.Where(p => 
                (p.RecordType ?? "In") == type &&
                (p.SerialNumber ?? "N/A") == p.SerialNumber &&
                (p.RequestedBy ?? "Unknown") == p.RequestedBy &&
                (p.RmaNumber ?? "N/A") == p.RmaNumber);

            if (startDate != null) products = products.Where(p => p.DateAdded >= startDate);
            if (endDate != null) products = products.Where(p => p.DateAdded <= endDate);

            if (!string.IsNullOrEmpty(searchString))
            {
                products = products.Where(p => 
                    p.Name!.Contains(searchString) || 
                    (p.Description != null && p.Description.Contains(searchString)));
            }

            if (!string.IsNullOrEmpty(category))
            {
                products = products.Where(p => p.Category == category);
            }

            products = sortOrder == "old" 
                ? products.OrderBy(p => p.DateAdded) 
                : products.OrderByDescending(p => p.DateAdded);

            return View(await products.ToListAsync());
        }

        [HttpGet]
        [Route("[controller]/Export")]
        public async Task<FileResult> Export(
            string type = "In",
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            startDate ??= DateTime.MinValue;
            endDate ??= DateTime.MaxValue;

            var data = await _context.Products
                .Where(p => (p.RecordType ?? "In") == type)
                .Where(p => p.DateAdded >= startDate && p.DateAdded <= endDate)
                .ToListAsync();

            var sb = new StringBuilder();
            sb.AppendLine("SerialNumber,ItemName,Category,Quantity,RequestedBy,RmaNumber,DateAdded");
            
            foreach (var record in data)
            {
                var values = new[]
                {
                    record.SerialNumber ?? "N/A",
                    record.Name ?? "Untitled",
                    record.Category ?? "Uncategorized",
                    record.Quantity.ToString(),
                    record.RequestedBy ?? "Unknown",
                    record.RmaNumber ?? "N/A",
                    (record.DateAdded == default ? DateTime.Now : record.DateAdded).ToString("yyyy-MM-dd")
                };

                sb.AppendLine(string.Join(",", values));
            }

            return File(
                Encoding.UTF8.GetBytes(sb.ToString()),
                "text/csv",
                $"Export-{type}-{DateTime.Now:yyyyMMdd}.csv"
            );
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create(string type = "In")
        {
            ViewBag.Type = type;
            return View(new Product { DateAdded = DateTime.UtcNow, RecordType = type });
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Category,Quantity,SerialNumber,RequestedBy,RmaNumber,RecordType,DateAdded")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: /Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();
            return View(product);
        }
        
        // POST: /Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Category,Quantity,SerialNumber,RequestedBy,RmaNumber,DateAdded,RecordType")] Product product)
        {
            if (id != product.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index), new { type = product.RecordType });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                        return NotFound();
                    throw;
                }
            }
            return View(product);
        }
        
        // GET: /Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var product = await _context.Products.FindAsync(id);
            if (product == null) return NotFound();
            return View(product);
        }
        
        // POST: /Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        
        private bool ProductExists(int id) =>
            _context.Products.Any(e => e.Id == id);
    }
}