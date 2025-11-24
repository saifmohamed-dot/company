using AutoMapper;
using company.DTO.EmployeeDtos;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.IRepositories;

namespace company.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _repo;
        private readonly IDepartmentRepository _deptRepo;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepository repo, IMapper mapper, IDepartmentRepository deptRepo)
        {
            _repo = repo;
            _mapper = mapper;
            _deptRepo = deptRepo;
        }

        // GET: api/employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeReadDto>>> GetAll()
        {
            var employees = await _repo.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<EmployeeReadDto>>(employees));
        }

        // GET: api/employee/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeReadDto>> GetById(int id)
        {
            var employee = await _repo.GetByIdAsync(id);
            if (employee == null) return NotFound();

            return Ok(_mapper.Map<EmployeeReadDto>(employee));
        }
        // GET: api/employee/with-department
        [HttpGet("with-clients")]
        public async Task<ActionResult<IEnumerable<EmployeeReadDto>>> GetAllWithClients()
        {
            var employees = await _repo.GetAllWithClientsAsync(track: false);

            return Ok(_mapper.Map<IEnumerable<EmployeeReadDto>>(employees));
        }

        // GET: api/employee/{id}/with-department
        [HttpGet("{id}/with-clients")]
        public async Task<ActionResult<EmployeeReadDto>> GetByIdWithClients(int id)
        {
            var employee = await _repo.GetByIdWithClientsAsync(id, track: false);

            if (employee == null) return NotFound();

            return Ok(_mapper.Map<EmployeeReadDto>(employee));
        }
        // GET: api/employee/with-department
        [HttpGet("with-department")]
        public async Task<ActionResult<IEnumerable<EmployeeReadDto>>> GetAllWithDepartment()
        {
            var employees = await _repo.GetAllWithDepartmentAsync(track: false);

            return Ok(_mapper.Map<IEnumerable<EmployeeReadDto>>(employees));
        }

        // GET: api/employee/{id}/with-department
        [HttpGet("{id}/with-department")]
        public async Task<ActionResult<EmployeeReadDto>> GetByIdWithDepartment(int id)
        {
            var employee = await _repo.GetByIdWithDepartmentAsync(id, track: false);

            if (employee == null) return NotFound();

            return Ok(_mapper.Map<EmployeeReadDto>(employee));
        }

        // POST: api/employee
        [HttpPost]
        public async Task<ActionResult<EmployeeReadDto>> Create(EmployeeCreateDto dto)
        {
            var employee = _mapper.Map<Employee>(dto);
            var dept = await _deptRepo.GetByIdAsync(dto.DepartmentId);
            if (dept == null) return NotFound("department not found");
            await _repo.CreateAsync(employee);

            return CreatedAtAction(
                nameof(GetById),
                new { id = employee.EmployeeId },
                _mapper.Map<EmployeeReadDto>(employee)
            );
        }

        // PUT: api/employee/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EmployeeUpdateDto dto)
        {
            if(id != dto.EmployeeId)
            {
                return BadRequest("ID mismatch");
            }
            if(id == -1)
            {
                return BadRequest("not allowed");
            }
            var existing = await _repo.GetByIdAsync(id);
            if (existing == null) return NotFound();
            var dept = await _deptRepo.GetByIdAsync(dto.DepartmentId);
            if (dept == null) return NotFound("department not found");

            _mapper.Map(dto, existing);
            await _repo.UpdateAsync(existing);

            return NoContent();
        }

        // DELETE: api/employee/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == -1) return BadRequest("not allowed");
            var employee = await _repo.GetByIdWithDepartmentAsync(id , true); // tracked cuz we need to rollback the client .
            if (employee == null) return NotFound();
            await _repo.RollBackClientAsync(employee.Clients);
            await _repo.DeleteAsync(employee);
            return NoContent();
        }
    }
}
