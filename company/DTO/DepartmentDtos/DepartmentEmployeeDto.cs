namespace company.DTO.DepartmentDtos
{
    // we make this principal / dependant dto to avoid the circular json serialization .
    public class DepartmentEmployeeDto
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
    }
}
