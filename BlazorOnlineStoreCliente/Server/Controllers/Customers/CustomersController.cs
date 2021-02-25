using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BlazorOnlineStoreCliente.Server.Data;
using BlazorOnlineStoreCliente.Shared.Models;
using BlazorOnlineStoreCliente.Server.Contracts;
using BlazorOnlineStoreCliente.Server.Helper;

namespace BlazorOnlineStoreCliente.Server.Controllers.Customers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {        
        private readonly ICustomerRepository _customerRepository;
        private readonly IFileStorageService _fileStorageService;

        public CustomersController(ICustomerRepository customerRepository, IFileStorageService fileStorageService)
        {            
            _customerRepository = customerRepository;
            _fileStorageService = fileStorageService;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            try
            {
                return Ok(await _customerRepository.GetAll());
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }
        }

        // GET: api/Customers/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            try
            {
                var customer = await _customerRepository.GetById(id);

                if (customer == null)
                {
                    return NotFound($"Customer with Id = {id} not found.");
                }

                return customer;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }
            
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Customer>> PutCustomer(int id, Customer customer)
        {       
            try
            {
                if (id != customer.CustomerID)
                {
                    return BadRequest();
                }

                var customerToUpdate = await _customerRepository.GetById(id);

                if (customerToUpdate == null)
                {
                    return NotFound($"Customer with Id = {id} not found.");
                }

                if (!string.IsNullOrWhiteSpace(customer.CustomerPhoto))
                {
                    var customerPhoto = Convert.FromBase64String(customer.CustomerPhoto);
                    customer.CustomerPhoto = await _fileStorageService.EditFile(customerPhoto, "jpg", "customer", customer.CustomerPhoto);
                }

                return await _customerRepository.Update(customer);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data.");
            }
           
        }

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            try
            {
                if (customer == null)
                {
                    return BadRequest("Id mismatch.");
                }

                if (!string.IsNullOrWhiteSpace(customer.CustomerPhoto))
                {
                    var CustomerPhoto = Convert.FromBase64String(customer.CustomerPhoto);
                    customer.CustomerPhoto = await _fileStorageService.SaveFile(CustomerPhoto, "jpg", "customer");
                }

                var createdCustomer = await _customerRepository.Add(customer);

                return CreatedAtAction(nameof(GetCustomer), new { id = createdCustomer.CustomerID }, createdCustomer);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating data.");
            }            
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Customer>> DeleteCustomer(int id)
        {
            try
            {
                var customer = await _customerRepository.GetById(id);
                if (customer == null)
                {
                    return NotFound($"Customer with Id = {id} not found.");
                }

                return await _customerRepository.Delete(id);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data.");
            }
            
        }

    }
}
