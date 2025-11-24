using System.ComponentModel.DataAnnotations;

namespace company.DTO.EmployeeDtos
{
    public class EmployeeUpdateDto
    {
        [Required(ErrorMessage = " Employee ID required ")]
        public int EmployeeId { get; set; }
        [Required(ErrorMessage = " Name Is Required ")]
        public string Name { get; set; }
        [Required(ErrorMessage = " Department ID required ")]
        public int DepartmentId { get; set; }
    }
}
