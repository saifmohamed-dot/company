using AutoMapper;
using company.DTO.ClientDtos;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.IRepositories;

namespace company.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _repo;
        private readonly IEmployeeRepository _EmpRepo;
        private readonly IMapper _mapper;

        public ClientController(IClientRepository repo, IMapper mapper , IEmployeeRepository eRepo)
        {
            _repo = repo;
            _EmpRepo = eRepo;
            _mapper = mapper;
        }

        // GET: api/client
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientReadDto>>> GetAll()
        {
            var clients = await _repo.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<ClientReadDto>>(clients));
        }

        // GET: api/client/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientReadDto>> GetById(int id)
        {
            var client = await _repo.GetByIdAsync(id);
            if (client == null) return NotFound();

            return Ok(_mapper.Map<ClientReadDto>(client));
        }
        // GET: api/client/with-details
        [HttpGet("with-employee")]
        public async Task<ActionResult<IEnumerable<ClientReadDto>>> GetAllWithEmployee()
        {
            var clients = await _repo.GetAllWithEmployeeAsync(track: false);
            return Ok(_mapper.Map<IEnumerable<ClientReadDto>>(clients));
        }

        // GET: api/client/{id}/with-details
        [HttpGet("{id}/with-employee")]
        public async Task<ActionResult<ClientReadDto>> GetByIdWithInclude(int id)
        {
            var client = await _repo.GetByIdEmployeeAsync(id, track: false);
            if (client == null) return NotFound();

            return Ok(_mapper.Map<ClientReadDto>(client));
        }

        // POST: api/client
        [HttpPost]
        public async Task<ActionResult<ClientReadDto>> Create(ClientCreateDto dto)
        {
            if(dto.EmployeeId == -1)
            {
                return BadRequest("not allowed");
            }
            var client = _mapper.Map<Client>(dto);
            var employee = await _EmpRepo.GetByIdAsync(dto.EmployeeId);
            if (employee == null) return NotFound("Employee Not Found");
            await _repo.CreateAsync(client);

            return CreatedAtAction(
                nameof(GetById),
                new { id = client.ClientId },
                _mapper.Map<ClientReadDto>(client)
            );
        }

        // PUT: api/client/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ClientUpdateDto dto)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return NotFound();

            _mapper.Map(dto, existing);
            await _repo.UpdateAsync(existing);

            return NoContent();
        }

        // DELETE: api/client/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var client = await _repo.GetByIdAsync(id);
            if (client == null) return NotFound();

            await _repo.DeleteAsync(client);
            return NoContent();
        }
    }
}
