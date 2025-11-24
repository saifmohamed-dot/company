namespace DataAccessLayer.Entities
{
    public class Client
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        // Employee relation
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}
