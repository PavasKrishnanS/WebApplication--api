using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApplication2.Models;
using WebApplication2.Repositories;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerRepository _repository;

        public CustomerController(CustomerRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var customers = await _repository.GetAllAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var customer = await _repository.GetByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest("Customer cannot be null.");
            }

            var createdCustomer = await _repository.AddAsync(customer);
            return CreatedAtAction(nameof(Get), new { id = createdCustomer.Id }, createdCustomer);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Customer customer)
        {
            if (customer == null || customer.Id != id)
            {
                return BadRequest("Customer cannot be null and ID must match.");
            }

            var isUpdated = await _repository.UpdateAsync(customer);
            if (!isUpdated)
            {
                return NotFound();
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _repository.DeleteAsync(id);
            if (!isDeleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
