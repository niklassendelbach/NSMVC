using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NSMVC.Data;
using NSMVC.Models;
using NSMVC.Repository;

namespace NSMVC.Controllers
{
    public class BookListsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICustomerRepository _db;
        public BookListsController(ApplicationDbContext context, ICustomerRepository db)
        {
            _context = context;
            _db = db;
        }

        //GET
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BookLists.Include(b => b.Books).Include(b => b.Customers).OrderBy(b => b.IsReturned);
            return View(await applicationDbContext.ToListAsync());
        }

        //CREATE
        public IActionResult Create()
        {
            ViewData["FK_BookId"] = new SelectList(_context.Books, "BookId", "Title");
            ViewData["FK_CustomerId"] = new SelectList(_context.Customers, "CustomerId", "FullName");
            return View();
        }

        //POST/CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLoan(BookList bookLoan)
        {
            await _db.AddBookLoan(bookLoan);
            if (bookLoan == null)
            {
                return BadRequest("Could not add a new loan");
            }
            return RedirectToAction(nameof(Index));
        }

        //MARKRETURN
        public async Task<IActionResult> MarkReturned(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var succesful = await _db.MarkReturned(id);
            if (!succesful)
            {
                return BadRequest("Could not mark book as returned");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
