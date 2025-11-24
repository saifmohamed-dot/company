using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.IRepositories;

namespace RepositoryLayer.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        readonly DbContext _dbContext;
        public EmployeeRepository(DbContext context) : base(context)
        {
            _dbContext = context;
        }
        public async Task<IEnumerable<Employee>> GetAllAsync(bool track)
        {
            if (track)
            {
                return await _dbContext.Set<Employee>().ToListAsync();
            }
            return await GetAllAsync(); // return the default ( Repository unTracked method ).
        }

        public async Task<IEnumerable<Employee>> GetAllWithClientsAsync(bool track)
        {
            if (track)
            {
                return await _dbContext.Set<Employee>().Include(e => e.Clients).ToListAsync();
            }
            return await _dbContext.Set<Employee>().AsNoTracking().Include(e => e.Clients).ToListAsync();
        }
        public async Task<IEnumerable<Employee>> GetAllWithDepartmentAsync(bool track)
        {
            if (track)
            {
                return await _dbContext.Set<Employee>()
                    .Include(e => e.Department)
                    .ToListAsync();
            }

            return await _dbContext.Set<Employee>()
                .AsNoTracking()
                .Include(e => e.Department)
                .ToListAsync();
        }
        public async Task<Employee> GetByIdAsync(int id, bool track)
        {
            if (track)
            {
                return await GetByIdAsync(id); // return the default ( Repository tracked method ).
            }
            return await _dbContext.Set<Employee>().AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<Employee> GetByIdWithClientsAsync(int id, bool track)
        {
            if (track)
            {
                return await _dbContext.Set<Employee>().Include(e => e.Clients).FirstOrDefaultAsync(e => e.EmployeeId == id);
            }
            return await _dbContext.Set<Employee>().AsNoTracking().Include(e => e.Clients).FirstOrDefaultAsync(e => e.EmployeeId == id);
        }
        public async Task<Employee> GetByIdWithDepartmentAsync(int id, bool track)
        {
            if (track)
            {
                return await _dbContext.Set<Employee>()
                    .Include(e => e.Department)
                    .FirstOrDefaultAsync(e => e.EmployeeId == id);
            }

            return await _dbContext.Set<Employee>()
                .AsNoTracking()
                .Include(e => e.Department)
                .FirstOrDefaultAsync(e => e.EmployeeId == id);
        }

        public async Task RollBackClientAsync(IEnumerable<Client> clients)
        {
            foreach (var client in clients)
            {
                client.EmployeeId = -1; // revert back to the temp employee .
            }
            await _dbContext.SaveChangesAsync(); // it has to be done before the principal delete ( avoiding conflict of cascade on delete ) .
        }
    }
}
