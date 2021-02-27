using BlazorOnlineStoreCliente.Server.Contracts;
using BlazorOnlineStoreCliente.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlazorOnlineStoreCliente.Server.Controllers.CardDetails
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardDetailsController : ControllerBase
    {
        private readonly ICardDetailRepository _cardDetailRepository;

        public CardDetailsController(ICardDetailRepository cardDetailRepository)
        {
            _cardDetailRepository = cardDetailRepository;
        }
        // GET: api/CardDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CardDetail>>> GetCardDetails()
        {
            try
            {
                return Ok(await _cardDetailRepository.GetAll());
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }
        }

        // GET: api/CardDetails/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CardDetail>> GetCardDetail(int id)
        {
            try
            {
                var cardDetail = await _cardDetailRepository.GetById(id);

                if (cardDetail == null)
                {
                    return NotFound($"CardDetail with Id = {id} not found.");
                }

                return cardDetail;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }

        }

        // PUT: api/CardDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        public async Task<ActionResult<CardDetail>> PutCardDetail(int id, CardDetail cardDetail)
        {
            try
            {
                if (id != cardDetail.CardDetailID)
                {
                    return BadRequest();
                }

                var cardDetailToUpdate = await _cardDetailRepository.GetById(id);

                if (cardDetailToUpdate == null)
                {
                    return NotFound($"CardDetail with Id = {id} not found.");
                }
               
                return await _cardDetailRepository.Update(cardDetail);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data.");
            }

        }

        // POST: api/CardDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CardDetail>> PostCardDetail(CardDetail cardDetail)
        {           
            try
            {
                if (cardDetail == null)
                {
                    return BadRequest("Id mismatch.");
                }

                var createdCardDetail = await _cardDetailRepository.Add(cardDetail);

                return CreatedAtAction(nameof(GetCardDetail), new { id = createdCardDetail.CardDetailID }, createdCardDetail);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating data.");
            }
        }

        // DELETE: api/CardDetails/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CardDetail>> DeleteCardDetail(int id)
        {
            try
            {
                var cardDetail = await _cardDetailRepository.GetById(id);
                if (cardDetail == null)
                {
                    return NotFound($"CardDetail with Id = {id} not found.");
                }

                return await _cardDetailRepository.Delete(id);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data.");
            }

        }

        // GET: api/cardDetails/searchKey
        [HttpGet("{searchKey}")]
        public async Task<ActionResult<IEnumerable<CardDetail>>> Search(string searchKey)
        {
            try
            {
                return Ok(await _cardDetailRepository.Search(searchKey));
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
            }
        }

    }
}
