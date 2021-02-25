using AutoMapper;
using BlazorOnlineStoreCliente.Server.Contracts;
using BlazorOnlineStoreCliente.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorOnlineStoreCliente.Server.Data
{
    public class SQLCustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SQLCustomerRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Customer> Add(Customer newEntity)
        {
            var customer = await _context.Customers.AddAsync(newEntity);
            await _context.SaveChangesAsync();

            return customer.Entity;
        }

        public async Task<Customer> Delete(int id)
        {
            var customerToDelete = await _context.Customers.FindAsync(id);

            if (customerToDelete != null)
            {
                _context.Customers.Remove(customerToDelete);
                await _context.SaveChangesAsync();
            }

            return customerToDelete;
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> GetById(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<IEnumerable<Customer>> Search(string searchKey)
        {
            return await _context.Customers.Where(cs => cs.CustomerAddress.Contains(searchKey) ||
                         cs.CustomerCity.Contains(searchKey) || cs.CustomerCountry.Contains(searchKey) ||
                         cs.CustomerName.Contains(searchKey) || cs.CustomerProvince.Contains(searchKey) ||
                         cs.Email.Contains(searchKey) || cs.Phone.Contains(searchKey)).ToListAsync();
        }

        public async Task<Customer> Update(Customer updatedEntity)
        {
            var result = await _context.Customers.FirstOrDefaultAsync(cs => cs.CustomerID == updatedEntity.CustomerID);

            _mapper.Map(updatedEntity, result);

            await _context.SaveChangesAsync();

            return result;
        }
    }
}
