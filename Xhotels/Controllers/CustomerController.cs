using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xhotels.Data.Models;
using Xhotels.Data.Repository;

namespace Xhotels.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerController(ICustomerRepository customerRepository) {
            _customerRepository = customerRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById([FromRoute] int id)
        {
            var customer = await _customerRepository.GetCustomerById(id);
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromBody] Customer customer)
        {
            var addedCustomerId = await _customerRepository.AddCustomer(customer);
            return Ok(addedCustomerId);
        }
    }
}
