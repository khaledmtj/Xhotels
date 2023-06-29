using Microsoft.EntityFrameworkCore;
using Xhotels.Data.Models;

namespace Xhotels.Data.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly HotelContext _context;
        public CustomerRepository(HotelContext hotelContext)
        {
            _context = hotelContext;
        }
        public async Task<int> AddCustomer(Customer customer)
        {
            var addedCustomer = await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            var addedCustomerId = addedCustomer.Entity.Id;
            return addedCustomerId;
        }

        public async Task<Customer?> GetCustomerById(int id)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
