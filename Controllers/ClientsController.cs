using Microsoft.AspNetCore.Mvc;
using CardStorage.Data.Entities;
using CardStorage.Data.UnitOfWork;
using AutoMapper;
using CardStorage.Data.Requests.ClientControllerRequests;
using Microsoft.AspNetCore.Authorization;

namespace CardStorage.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClientsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: api/Clients
        [HttpGet]
        public ActionResult<IEnumerable<Client>> GetClients()
        {
            return Ok(_unitOfWork.Clients.GetAll());
        }

        // GET: api/Clients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            var client = await _unitOfWork.Clients.GetByIdAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        // POST: api/Clients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Client>> PostClient(ClientPostRequest request)
        {
            await _unitOfWork.Clients.InsertAsync(_mapper.Map<Client>(request));

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
        public async Task<IActionResult> DeleteClient(int id)
        {
            _unitOfWork.Clients.Delete(new Client() { Id = id});

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
