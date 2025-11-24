using System.ComponentModel.DataAnnotations;

namespace company.DTO.DepartmentDtos
{
    public class DepartmentUpdateDto
    {
        [Required(ErrorMessage = "Id Required")]
        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "Name Required")]
        public string Name { get; set; }
    }
}
