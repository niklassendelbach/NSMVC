using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using NSMVC.Models;
using NSMVC.Repository;

namespace NSMVC.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _context;
        public CustomerController(ICustomerRepository context)
        {
            _context = context;
        }
        //GET SearchString
        public async Task<ActionResult> GetCustomer(String SearchString)
        {
            IEnumerable<Customer> customerList = await _context.GetAllAsync();
            if (!String.IsNullOrEmpty(SearchString))
            {
                return View("GetCustomer", customerList.Where(c => c.FullName!.ToLower().Contains(SearchString.ToLower())));
            }
            
            return View(customerList);
        }
        //CREATE
        public IActionResult CreateCustomer()
        {
            return View();
        }
        //POST CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCustomer([Bind("CustomerId, FirstName, LastName, Adress, City")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                await _context.CreateAsync(customer);
                return RedirectToAction(nameof(GetCustomer));
            }
            return View(customer);
        }
        //EDIT
        public async Task<IActionResult> EditCustomer(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var customer = await _context.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }
        //POST EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCustomer(int id, [Bind("CustomerId, FirstName, LastName, Adress, City")] Customer customer)
        {
            if (id != customer.CustomerId)    
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    await _context.UpdateAsync(customer);
                    await _context.SaveAsync();
                }
                catch (Exception)
                {
                    if (customer == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(GetCustomer));
            }
            return View(customer);
        }
        //DELETE
        public async Task<IActionResult> DeleteCustomer(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var customer = await _context.GetAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }
        //POST DELETE
        [HttpPost, ActionName("DeleteCustomer")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context == null)
            {
                return NotFound();
            }
            var customer = await _context.FindAsync(id);
            if (customer != null)
            {
                await _context.RemoveAsync(customer);
            }
            return RedirectToAction(nameof(GetCustomer));
        }
        //ADD EDIT & DELETE
    }
}
