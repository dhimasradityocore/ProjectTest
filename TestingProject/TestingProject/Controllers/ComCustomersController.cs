using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestingProject.Data;
using TestingProject.Models;

namespace TestingProject.Controllers
{
    public class ComCustomersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ComCustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ComCustomers
        public async Task<IActionResult> Index()
        {
              return _context.comCustomers != null ? 
                          View(await _context.comCustomers.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.comCustomers'  is null.");
        }

        // GET: ComCustomers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.comCustomers == null)
            {
                return NotFound();
            }

            var comCustomer = await _context.comCustomers
                .FirstOrDefaultAsync(m => m.ComCustomerID == id);
            if (comCustomer == null)
            {
                return NotFound();
            }

            return View(comCustomer);
        }

        // GET: ComCustomers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ComCustomers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComCustomerID,CustomerName")] ComCustomer comCustomer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comCustomer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(comCustomer);
        }

        // GET: ComCustomers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.comCustomers == null)
            {
                return NotFound();
            }

            var comCustomer = await _context.comCustomers.FindAsync(id);
            if (comCustomer == null)
            {
                return NotFound();
            }
            return View(comCustomer);
        }

        // POST: ComCustomers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ComCustomerID,CustomerName")] ComCustomer comCustomer)
        {
            if (id != comCustomer.ComCustomerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comCustomer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComCustomerExists(comCustomer.ComCustomerID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(comCustomer);
        }

        // GET: ComCustomers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.comCustomers == null)
            {
                return NotFound();
            }

            var comCustomer = await _context.comCustomers
                .FirstOrDefaultAsync(m => m.ComCustomerID == id);
            if (comCustomer == null)
            {
                return NotFound();
            }

            return View(comCustomer);
        }

        // POST: ComCustomers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.comCustomers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.comCustomers'  is null.");
            }
            var comCustomer = await _context.comCustomers.FindAsync(id);
            if (comCustomer != null)
            {
                _context.comCustomers.Remove(comCustomer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComCustomerExists(int id)
        {
          return (_context.comCustomers?.Any(e => e.ComCustomerID == id)).GetValueOrDefault();
        }
    }
}
