using Microsoft.AspNetCore.Mvc;
using CardStorage.Data.Entities;
using CardStorage.Data.UnitOfWork;
using AutoMapper;
using CardStorage.Data.Requests.CardsControllerRequests;
using Microsoft.AspNetCore.Authorization;

namespace CardStorage.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientCardsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClientCardsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/Cards
        [HttpGet]
        public ActionResult<IEnumerable<ClientCard>> GetCards()
        {
            return Ok(_unitOfWork.Cards.GetAll());
        }

        // GET: api/Clients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientCard>> GetCard(int id)
        {
            var card = await _unitOfWork.Cards.GetByIdAsync(id);

            if (card == null)
            {
                return NotFound();
            }

            return Ok(card);
        }

        // POST: api/Cards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClientCard>> PostClient(CardsPostRequest request)
        {
            await _unitOfWork.Cards.InsertAsync(_mapper.Map<ClientCard>(request));

            try
            {
                await _unitOfWork.SaveAsync();
            }
            catch (Exception)
            {
                return Problem();
            }

            return NoContent();
        }

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(int id)
        {
            _unitOfWork.Cards.Delete(new ClientCard() { Id = id});

            try
            {
                await _unitOfWork.SaveAsync();
            }
            catch (Exception)
            {
                return Problem();
            }

            return NoContent();
        }
    }
}
