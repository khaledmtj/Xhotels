using Xhotels.Data.Models;

namespace Xhotels.Data.Repository
{
    public interface ICustomerRepository
    {
        public Task<Customer?> GetCustomerById(int id);
        public Task<int> AddCustomer(Customer customer);
    }
}
