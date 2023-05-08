using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using NSMVC.Data;
using NSMVC.Models;
using System.Linq;
using System.Linq.Expressions;

namespace NSMVC.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _db;
        public CustomerRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task CreateAsync(Customer entity)
        {
            await _db.Customers.AddAsync(entity);
            await SaveAsync();
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            return await _db.Customers.ToListAsync();

        }
        public async Task<Customer> GetAsync(int? id)
        {
            return await _db.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);
        }
        public async Task RemoveAsync(Customer entity)
        {
            _db.Remove(entity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Customer entity)
        {
            _db.Update(entity);
            await SaveAsync();
        }
        public async Task<Customer> FindAsync(int? id)
        {
            return await _db.Customers.FindAsync(id);
        }
        public async Task AddBookLoan(BookList newLoan)
        {
            newLoan.ReturnAt = DateTime.Now.AddDays(21);
            newLoan.IsReturned = false;
            await _db.BookLists.AddAsync(newLoan);
            await SaveAsync();
        }
        public async Task<bool> MarkReturned(int? id)
        {
            var book = await _db.BookLists
                .Where(x => x.BookListId == id)
                .SingleOrDefaultAsync();
            if (book == null) return false;
            book.IsReturned = true;
            book.ReturnDate = DateTime.Now;
            var save = await _db.SaveChangesAsync();
            return save == 1;
        }
    }
}
