namespace DataAccessLayer.Entities
{
    public class Department
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }

        // Navigation: One Department → Many Employees
        public ICollection<Employee> Employees { get; set; } = new List<Employee>(); // init with empty list in case there is not employees in this department .
    }
}
