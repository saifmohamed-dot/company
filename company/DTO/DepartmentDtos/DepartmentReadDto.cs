using company.DTO.EmployeeDtos;

namespace company.DTO.DepartmentDtos
{
    public class DepartmentReadDto
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public IEnumerable<DepartmentEmployeeDto> Employees { get; set; }
    }
}
