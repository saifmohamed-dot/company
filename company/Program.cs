using company;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.IRepositories;
using RepositoryLayer.Repositories;
using Polly;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(typeof(MappingConfig));

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<CompanyDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<DbContext, CompanyDbContext>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = ""; // Swagger at root: https://localhost:5001/
});
app.UseDeveloperExceptionPage();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CompanyDbContext>();
    var retry = Policy.Handle<SqlException>()
                  .WaitAndRetry(5, retryAttempt => TimeSpan.FromSeconds(5));

    retry.Execute(() => db.Database.Migrate()); // preventing migration on unstarted database server ( keep trying ) to ensure database server up and running .
    Console.WriteLine("Migration Done");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
