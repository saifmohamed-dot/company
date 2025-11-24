

namespace company.DTO.EmployeeDtos
{
    public class EmployeeReadDto
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }

        public EmployeeDepartmentDto Department { get; set; }
        // we should seperate each of included dto (empDeptIncludedDto and empDeptIncludedClient) .
        public IEnumerable<EmployeeClientDto> Clients { get; set; }
    }
}
