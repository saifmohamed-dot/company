using System.ComponentModel.DataAnnotations;

namespace company.DTO.EmployeeDtos
{
    // we make this principal / dependant dto to avoid the circular json serialization .
    public class EmployeeClientDto
    {

        public int ClientId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }
    }
}
