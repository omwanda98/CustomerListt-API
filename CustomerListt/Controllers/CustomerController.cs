using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerListt.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("GetCustomers")]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await _customerService.GetCustomersAsync();
            if (customers == null || customers.Count == 0)
            {
                return NotFound("No customers found.");
            }

            return Ok(customers);
        }
        [HttpGet("GetCustomer/{customerNo}")]
        public async Task<IActionResult> GetCustomerByNumber(string customerNo)
        {
            var customers = await _customerService.GetCustomersAsync();
            var customer = customers.FirstOrDefault(c => c.No == customerNo);

            if (customer == null)
            {
                return NotFound($"Customer with number {customerNo} not found.");
            }

            return Ok(customer);
        }
    }
}

