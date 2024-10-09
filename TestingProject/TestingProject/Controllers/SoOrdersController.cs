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
    public class SoOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SoOrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SoOrders
        public async Task<IActionResult> Index()
        {
              return _context.SoOrders != null ?
                           View(await _context.SoOrders.Include(o => o.ComCustomer).ToListAsync()) :
                            Problem("Entity set 'ApplicationDbContext.SoOrders' is null.");
        }

        // GET: SoOrders/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.SoOrders == null)
            {
                return NotFound();
            }

            var soOrder = await _context.SoOrders
                .FirstOrDefaultAsync(m => m.SoOrderId == id);
            if (soOrder == null)
            {
                return NotFound();
            }

            return View(soOrder);
        }

        // GET: SoOrders/Create
        public async Task<IActionResult> CreateAsync()
        {
            var soOrder = new SoOrder
            {
                OrderDate = DateTime.Today,
                SoItems = new List<SoItem>()
            };
            
            var customers = _context.comCustomers.ToList(); 

            
            var customerList = new List<SelectListItem>
                 {
                     new SelectListItem { Value = "0", Text = "Select a Customer" }
                 };
            customerList.AddRange(customers.Select(c => new SelectListItem
            {
                Value = c.ComCustomerID.ToString(),
                Text = c.CustomerName
            }));

            ViewBag.CustomerList = customerList;

            return View(soOrder);
        }

        // POST: SoOrders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("SoOrderId,OrderNo,OrderDate,ComCustomerId,Address")] SoOrder soOrder)
        {
           
                _context.Add(soOrder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            var customers = await _context.comCustomers.ToListAsync();

            return View(soOrder);
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrder(SoOrder soOrder)
        {
            
                
                if (soOrder.SoOrderId > 0)
                {
                    _context.SoOrders.Update(soOrder); 
                }
                else
                {
                    await _context.SoOrders.AddAsync(soOrder); 
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); 
            

            
            return View(soOrder);
        }

        //}

        // GET: SoOrders/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.SoOrders == null)
            {
                return NotFound();
            }

            

            var soOrder = await _context.SoOrders.Include(o => o.SoItems).FirstOrDefaultAsync(o => o.SoOrderId == id);
            if (soOrder == null)
            {
                return NotFound();
            }
            var customers = _context.comCustomers.ToList();
            

            var customerList = new List<SelectListItem>
                 {
                     new SelectListItem { Value = "0", Text = "Select a Customer" }
                 };
            customerList.AddRange(customers.Select(c => new SelectListItem
            {
                Value = c.ComCustomerID.ToString(),
                Text = c.CustomerName
            }));

            
            ViewBag.CustomerList = customerList;
            // ViewBag.CustomerList = new SelectList(await _context.comCustomers.ToListAsync(), "ComCustomerId", "CustomerName", soOrder.ComCustomerId);


            return View(soOrder);
        }

        // POST: SoOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SoOrder model)
        {
            // Check if the provided ID matches the model's ID
            if (id != model.SoOrderId)
            {
                return NotFound();
            }

            // Retrieve the existing order with its items from the database
            var existingOrder = await _context.SoOrders
                                               .Include(o => o.SoItems)
                                               .FirstOrDefaultAsync(o => o.SoOrderId == id);
            if (existingOrder == null)
            {
                return NotFound();
            }

            // Clear the existing items first
            _context.SoItems.RemoveRange(existingOrder.SoItems);

            // Update the order details
            existingOrder.OrderNo = model.OrderNo;
            existingOrder.OrderDate = model.OrderDate;
            existingOrder.ComCustomerId = model.ComCustomerId;
            existingOrder.Address = model.Address;

            // Add the new items from the model
            foreach (var item in model.SoItems)
            {
                // Ensure that each item's ID is set to zero so that it will be treated as a new item
                item.SoItemId = 0; // Assuming 0 is used for new items
                existingOrder.SoItems.Add(item);
            }

            // Save the changes to the database
            await _context.SaveChangesAsync();

            // Redirect to the index after successful update
            return RedirectToAction(nameof(Index));
        }


        //[HttpPost]
        //public async Task<IActionResult> Edit(SoOrder soOrder)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Update the order in the database
        //        _context.Update(soOrder);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    // If ModelState is not valid, fetch the CustomerList again to populate the dropdown
        //    ViewBag.CustomerList = new SelectList(_context.comCustomers, "ComCustomerId", "CustomerName");
        //    return View(soOrder);
        //}

        // GET: SoOrders/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.SoOrders == null)
            {
                return NotFound();
            }

            var soOrder = await _context.SoOrders
                .FirstOrDefaultAsync(m => m.SoOrderId == id);
            if (soOrder == null)
            {
                return NotFound();
            }

            return View(soOrder);
        }

        // POST: SoOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.SoOrders == null)
            {
                return Problem("Entity set 'ApplicationDbContext.SoOrders'  is null.");
            }
            var soOrder = await _context.SoOrders.FindAsync(id);
            if (soOrder != null)
            {
                _context.SoOrders.Remove(soOrder);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SoOrderExists(long id)
        {
          return (_context.SoOrders?.Any(e => e.SoOrderId == id)).GetValueOrDefault();
        }
    }
}
