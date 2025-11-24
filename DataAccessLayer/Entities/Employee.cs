namespace DataAccessLayer.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }

        // Department relation
        public int DepartmentId { get; set; }  
        public Department Department { get; set; }

        // Navigation: One Employee → Many Clients
        public ICollection<Client> Clients { get; set; } = new List<Client>();
    }
}
