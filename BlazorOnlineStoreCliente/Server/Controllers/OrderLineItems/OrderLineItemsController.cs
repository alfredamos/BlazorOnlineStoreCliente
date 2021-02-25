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

namespace BlazorOnlineStoreClient.Server.Controllers.OrderLineItems
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderLineItemsController : ControllerBase
    {        
        private readonly IOrderLineItemRepository _orderLineItemRepository;

        public OrderLineItemsController(IOrderLineItemRepository orderLineItemRepository)
        {            
            _orderLineItemRepository = orderLineItemRepository;
        }

        // GET: api/OrderLineItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderLineItem>>> GetOrderLineItems()
        {
            try
            {
                return Ok(await _orderLineItemRepository.GetAll());
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }
        }

        // GET: api/OrderLineItems/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<OrderLineItem>> GetOrderLineItem(int id)
        {
            try
            {
                var orderLineItem = await _orderLineItemRepository.GetById(id);

                if (orderLineItem == null)
                {
                    return NotFound($"OrderLineItem with Id = {id} not found.");
                }

                return orderLineItem;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }
            
        }

        // PUT: api/OrderLineItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        public async Task<ActionResult<OrderLineItem>> PutOrderLineItem(int id, OrderLineItem orderLineItem)
        {
            try
            {
                if (id != orderLineItem.OrderLineItemID)
                {
                    return BadRequest("Id mismatch.");
                }

                var orderLineItemToUpdate = await _orderLineItemRepository.GetById(id);

                if (orderLineItemToUpdate == null)
                {
                    return NotFound($"OrderLineItem with Id = {id} not found.");
                }

                return await _orderLineItemRepository.Update(orderLineItem);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data.");
            }           
                      
        }

        // POST: api/OrderLineItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderLineItem>> PostOrderLineItem(OrderLineItem orderLineItem)
        {
            try
            {
                if (orderLineItem == null)
                {
                    return BadRequest("Invalid input");
                }

                var createdOrderLineItem = await _orderLineItemRepository.Add(orderLineItem);

                return CreatedAtAction(nameof(GetOrderLineItem), new { id = createdOrderLineItem.OrderLineItemID }, createdOrderLineItem);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating data.");
            }            
        }

        // DELETE: api/OrderLineItems/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<OrderLineItem>> DeleteOrderLineItem(int id)
        {
            try
            {
                var orderLineItem = await _orderLineItemRepository.GetById(id);

                if (orderLineItem == null)
                {
                    return NotFound($"OrderLineItem with Id = {id} not found.");
                }

                return await _orderLineItemRepository.Delete(id);

            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating data.");
            }
        }

        // GET: api/OrderLineItems
        [HttpGet("{searchKey}")]
        public async Task<ActionResult<IEnumerable<OrderLineItem>>> Search(string searchKey)
        {
            try
            {
                return Ok(await _orderLineItemRepository.Search(searchKey));
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }
        }

    }
}
