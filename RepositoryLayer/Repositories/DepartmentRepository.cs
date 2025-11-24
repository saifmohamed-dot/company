using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.IRepositories;
namespace RepositoryLayer.Repositories
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        readonly DbContext _dbContext;
        public DepartmentRepository(DbContext context) : base(context)
        {
            _dbContext = context;
        }
        public async Task<IEnumerable<Department>> GetAllAsync(bool track)
        {
            if (track)
            {
                return await _dbContext.Set<Department>().ToListAsync();
            }
            return await GetAllAsync(); // return the default ( Repository unTracked method ).
        }

        public async Task<IEnumerable<Department>> GetAllEmployeesAsync(bool track)
        {
            if (track)
            {
                return await _dbContext.Set<Department>().Include(d => d.Employees).ToListAsync();
            }
            return await _dbContext.Set<Department>().AsNoTracking().Include(c => c.Employees).ToListAsync();
        }

        public async Task<Department> GetByIdAsync(int id, bool track)
        {
            if (track)
            {
                return await GetByIdAsync(id); // return the default ( Repository tracked method ).
            }
            return await _dbContext.Set<Department>().AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<Department> GetByIdWithEmployeesAsync(int id, bool track)
        {
            if (track)
            {
                return await _dbContext.Set<Department>().Include(d => d.Employees).FirstOrDefaultAsync(d => d.DepartmentId == id);
            }
            return await _dbContext.Set<Department>().AsNoTracking().Include(d => d.Employees).FirstOrDefaultAsync(d => d.DepartmentId == id);
        }

        public async Task RollBackAsync(IEnumerable<Employee> employees)
        {
            foreach (var employee in employees)
            {
                employee.DepartmentId = -1; // revert back to the general department .
            }
            await _dbContext.SaveChangesAsync(); // it has to be done before the principal delete ( avoiding conflict of cascade on delete ) .
        }
    }
}
