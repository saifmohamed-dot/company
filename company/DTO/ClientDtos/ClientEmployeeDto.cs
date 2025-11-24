using System.ComponentModel.DataAnnotations;

namespace company.DTO.ClientDtos
{
    // we make this dependant /  principal dto to avoid the circular json serialization .
    public class ClientEmployeeDto
    {
        
        public int EmployeeId { get; set; }
        public string Name { get; set; }
    }
}
