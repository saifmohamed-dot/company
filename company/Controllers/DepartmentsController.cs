using AutoMapper;
using company.DTO.DepartmentDtos;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.IRepositories;
using System.Collections.Specialized;

namespace company.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        readonly IDepartmentRepository _repo;
        readonly IMapper _mapper;

        public DepartmentsController(IDepartmentRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET: api/departments
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var departments = await _repo.GetAllAsync();
            var dto = _mapper.Map<IEnumerable<DepartmentReadDto>>(departments);
            return Ok(dto);
        }

        // GET: api/departments/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var department = await _repo.GetByIdAsync(id);
            if (department == null)
                return NotFound();

            var dto = _mapper.Map<DepartmentReadDto>(department);
            return Ok(dto);
        }
        // GET: api/department/with-employees
        [HttpGet("with-employees")]
        public async Task<ActionResult<IEnumerable<DepartmentReadDto>>> GetAllWithEmployees()
        {
            var departments = await _repo.GetAllEmployeesAsync(track: false);

            return Ok(_mapper.Map<IEnumerable<DepartmentReadDto>>(departments));
        }

        // GET: api/department/{id}/with-employees
        [HttpGet("{id}/with-employees")]
        public async Task<ActionResult<DepartmentReadDto>> GetByIdWithEmployees(int id)
        {
            var department = await _repo.GetByIdWithEmployeesAsync(id, track: false);

            if (department == null) return NotFound();

            return Ok(_mapper.Map<DepartmentReadDto>(department));
        }

        // POST: api/departments
        [HttpPost]
        public async Task<IActionResult> Create(DepartmentCreateDto dto)
        {
            var department = _mapper.Map<Department>(dto);
            await _repo.CreateAsync(department);

            return CreatedAtAction(nameof(GetById),
                new { id = department.DepartmentId },
                _mapper.Map<DepartmentReadDto>(department)); // return the route of the newly created department .
        }

        // PUT: api/departments/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DepartmentUpdateDto dto)
        {
            if (id != dto.DepartmentId || id == -1)
                return BadRequest("ID mismatch.");

            var department = await _repo.GetByIdAsync(id);
            if (department == null)
                return NotFound();

            _mapper.Map(dto, department);

            await _repo.UpdateAsync(department);

            return NoContent();
        }

        // DELETE: api/departments/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var department = await _repo.GetByIdWithEmployeesAsync(id, true); // tracked cuz we need to rollback the employee .
            if (department == null || id == -1) // prevent anyone from deleting the gehenral department .
                return NotFound();
            await _repo.RollBackAsync(department.Employees);
            await _repo.DeleteAsync(department);
            return NoContent();
        }
    }
}
