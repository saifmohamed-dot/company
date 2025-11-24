using System.ComponentModel.DataAnnotations;

namespace company.DTO.DepartmentDtos
{
    public class DepartmentCreateDto
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
    }
}
