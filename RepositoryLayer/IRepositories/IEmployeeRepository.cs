using DataAccessLayer.Entities;
namespace RepositoryLayer.IRepositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        // Add employee-specific async methods if needed
        Task<Employee> GetByIdAsync(int id, bool track);
        Task<IEnumerable<Employee>> GetAllAsync(bool track);
        Task<Employee> GetByIdWithClientsAsync(int id, bool track); // we could instead pass a string with the property name to include .
        Task<IEnumerable<Employee>> GetAllWithClientsAsync(bool track);
        Task<Employee> GetByIdWithDepartmentAsync(int id, bool track);
        Task<IEnumerable<Employee>> GetAllWithDepartmentAsync(bool track);
        Task RollBackClientAsync(IEnumerable<Client> clients);
    }
}
