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
using Microsoft.AspNetCore.Authorization;

namespace BlazorOnlineStoreClient.Server.Controllers.Products
{
    [Route("api/[controller]")]
    [ApiController]    
    public class ProductsController : ControllerBase
    {        
        private readonly IProductRepository _productRepository;
        private readonly IFileStorageService _fileStorageService;

        public ProductsController(IProductRepository productRepository, IFileStorageService fileStorageService)
        {            
            _productRepository = productRepository;
            _fileStorageService = fileStorageService;
        }

        // GET: api/Orders
        [HttpGet]        
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            try
            {
                return Ok(await _productRepository.GetAll());
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database.");
            }
        }

        // GET: api/Orders/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            try
            {
                var product = await _productRepository.GetById(id);

                if (product == null)
                {
                    return NotFound($"Order with Id = {id} not found.");
                }

                return product;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database.");
            }
            
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Product>> PutProduct(int id, Product product)
        {
            try
            {
                if (id != product.ProductID)
                {
                    return BadRequest("Id mismatch.");
                }

                var productToUpdate = await _productRepository.GetById(id);

                if (productToUpdate == null)
                {
                    return NotFound($"Order with Id = {id} not found.");
                }
               
                if (!string.IsNullOrWhiteSpace(product.ImageLink))
                {                    
                    var productImageLink = Convert.FromBase64String(product.ImageLink);                                       
                    product.ImageLink = await _fileStorageService.EditFile(productImageLink, "jpg", "product", product.ImageLink);                    
                }

                var productUpdated = await _productRepository.Update(product);

                return productUpdated;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data from database.");
            }
        
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequest("Invalid input");
                }

                if (!string.IsNullOrWhiteSpace(product.ImageLink))
                {
                    var productImageLink = Convert.FromBase64String(product.ImageLink);
                    product.ImageLink = await _fileStorageService.SaveFile(productImageLink, "jpg", "product");
                }

                var createdProduct = await _productRepository.Add(product);

                return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.ProductID }, createdProduct);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating data from database.");
            }
            
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            try
            {
                var productToDelete = await _productRepository.GetById(id);
                if (productToDelete == null)
                {
                    return NotFound($"Order with Id = {id} not found.");
                }

                return await _productRepository.Delete(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data from database.");
            }
           
        }

        // GET: api/products/searchKey
        [HttpGet("{searchKey}")]
        public async Task<ActionResult<IEnumerable<Product>>> Search(string searchKey)
        {
            try
            {
                return Ok(await _productRepository.Search(searchKey));
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from database.");
            }
        }

    }
}
