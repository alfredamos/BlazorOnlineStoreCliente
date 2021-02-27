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

namespace BlazorOnlineStoreClient.Server.Controllers.Orders
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {        
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {            
            _orderRepository = orderRepository;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            try
            {
                return Ok(await _orderRepository.GetAll());
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
            }
        }

        // GET: api/Orders/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            try
            {
                var order = await _orderRepository.GetById(id);

                if (order == null)
                {
                    return NotFound($"Order with Id = {id} not found.");
                }

                return order;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
            }
            
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Order>> PutOrder(int id, Order order)
        {
            try
            {
                if (id != order.OrderID)
                {
                    return BadRequest("Id mismatch.");
                }

                var orderToUpdate = await _orderRepository.GetById(id);

                if (orderToUpdate == null)
                {
                    return NotFound($"Order with Id = {id} not found.");
                }

                return await _orderRepository.Update(order);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data.");
            }
                        
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            try
            {
                if (order == null)
                {
                    return BadRequest("Invalid input.");
                }
                var createdOrder = await _orderRepository.Add(order);
                return CreatedAtAction(nameof(GetOrder), new { id = createdOrder.OrderID }, createdOrder);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating data.");
            }
            
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Order>> DeleteOrder(int id)
        {
            try
            {
                var order = await _orderRepository.GetById(id);

                if (order == null)
                {
                    return NotFound($"Order with Id = {id} not found.");
                }

                return await _orderRepository.Delete(id);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data from database.");
            }
        }

        // GET: api/Orders/searchKey
        [HttpGet("{searchKey}")]
        public async Task<ActionResult<IEnumerable<Order>>> Search(string searchKey)
        {
            try
            {
                return Ok(await _orderRepository.Search(searchKey));
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
            }
        }

    }
}
