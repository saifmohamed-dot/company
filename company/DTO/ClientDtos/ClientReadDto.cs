namespace company.DTO.ClientDtos
{
    public class ClientReadDto
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ClientEmployeeDto Employee { get; set; }
    }
}
