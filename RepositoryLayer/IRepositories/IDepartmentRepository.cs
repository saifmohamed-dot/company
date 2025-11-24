using DataAccessLayer.Entities;

namespace RepositoryLayer.IRepositories
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        // Add department-specific async methods if needed
        Task<Department> GetByIdAsync(int id, bool track);
        Task<IEnumerable<Department>> GetAllAsync(bool track);
        Task<Department> GetByIdWithEmployeesAsync(int id , bool track);
        Task<IEnumerable<Department>> GetAllEmployeesAsync(bool track);
        Task RollBackAsync(IEnumerable<Employee> employees);
    }
}
