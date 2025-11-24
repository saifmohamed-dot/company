using System.ComponentModel.DataAnnotations;

namespace company.DTO.EmployeeDtos
{
    public class EmployeeCreateDto
    {
        [Required(ErrorMessage = " Name Required ")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Id Required")]
        public int DepartmentId { get; set; }
    }
}
