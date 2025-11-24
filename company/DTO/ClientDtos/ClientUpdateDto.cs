using System.ComponentModel.DataAnnotations;

namespace company.DTO.ClientDtos
{
    public class ClientUpdateDto
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Email must be a valid email address.")]
        public string Email { get; set; }
    }
}
