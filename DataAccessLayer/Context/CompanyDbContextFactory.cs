using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataAccessLayer.Context
{
    public class CompanyDbContextFactory : IDesignTimeDbContextFactory<CompanyDbContext>
    {
        public CompanyDbContext CreateDbContext(string[] args)
        {
            var optionbuilder = new DbContextOptionsBuilder<CompanyDbContext>();
            // will pass the connection string hard coded 
            // just for local migration migrations purpose.
            // but in the runtime we will depend on the appsettings.json
            optionbuilder.UseSqlServer("Data Source=.;Initial Catalog=CompanyDB;Integrated Security=True;TrustServerCertificate=True");
            return new CompanyDbContext(optionbuilder.Options);
        }
    }
}
