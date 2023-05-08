using NSMVC.Models;
using System.Linq.Expressions;

namespace NSMVC.Repository
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllAsync();
        Task<Customer> GetAsync(int? id);
        Task CreateAsync(Customer entity);
        Task UpdateAsync(Customer entity);
        Task RemoveAsync(Customer entity);
        Task AddBookLoan(BookList entity);
        Task<bool> MarkReturned(int? id);
        Task<Customer> FindAsync(int? id);
        Task SaveAsync();
    }
}
