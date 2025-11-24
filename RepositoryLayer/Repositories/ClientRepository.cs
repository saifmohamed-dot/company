using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.IRepositories;

namespace RepositoryLayer.Repositories
{
    public class ClientRepository : Repository<Client>, IClientRepository
    {
        readonly DbContext _dbContext;
        public ClientRepository(DbContext context) : base(context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<Client>> GetAllAsync(bool track)
        {
            if(track)
            {
                return await _dbContext.Set<Client>().ToListAsync();
            }
            return await GetAllAsync(); // return the default ( Repository unTracked method ).
        }

        public async Task<IEnumerable<Client>> GetAllWithEmployeeAsync(bool track)
        {
            if (track)
            {
                return await _dbContext.Set<Client>().Include(c => c.Employee).ToListAsync();
            }
            return await _dbContext.Set<Client>().AsNoTracking().Include(c => c.Employee).ToListAsync();
        }

        public async Task<Client> GetByIdAsync(int id, bool track)
        {
            if(track)
            {
                return await GetByIdAsync(id); // return the default ( Repository tracked method ).
            }
            return await _dbContext.Set<Client>().AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<Client> GetByIdEmployeeAsync(int id, bool track)
        {
            if(track)
            {
                return await _dbContext.Set<Client>().Include(c => c.Employee).FirstOrDefaultAsync(c => c.ClientId == id);
            }
            return await _dbContext.Set<Client>().AsNoTracking().Include(c => c.Employee).FirstOrDefaultAsync(c => c.ClientId == id);
        }
    }
}
