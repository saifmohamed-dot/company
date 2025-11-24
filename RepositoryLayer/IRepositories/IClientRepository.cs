using DataAccessLayer.Entities;
namespace RepositoryLayer.IRepositories
{
    public interface IClientRepository : IRepository<Client>
    {
        // Add client-specific async methods if needed
        Task<Client> GetByIdAsync(int id , bool track);
        Task<IEnumerable<Client>> GetAllAsync(bool track);
        Task<Client> GetByIdEmployeeAsync(int id , bool track);
        Task<IEnumerable<Client>> GetAllWithEmployeeAsync(bool track);
    }
}
