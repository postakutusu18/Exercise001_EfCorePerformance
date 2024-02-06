using Dapper;
using Exercise001_EfCorePerformance;
using Exercise001_EfCorePerformance.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DatabaseContext>(
    o => o.UseSqlServer(builder.Configuration.GetConnectionString("Database")));
var app = builder.Build();
app.UseHttpsRedirection();

app.MapPut("increase-salaries", async (int companyId, DatabaseContext dbContext) => {

var company= await dbContext.Set<Company>()
    .Include(c=>c.Employees)
    .FirstOrDefaultAsync(c=>c.Id==companyId);
    if (company is null)
    {
        return Results.NotFound($"The Company with Id '{companyId}' was not found");
    }

    foreach (var employee in company.Employees)
    {
        employee.Salary *= 1.1m;
    }
    company.LastSalaryUpdateUtc= DateTime.UtcNow;
    await dbContext.SaveChangesAsync();

    return Results.NoContent();
});

app.MapPut("increase-salaries-sql", async (int companyId, DatabaseContext dbContext) => {

    var company = await dbContext.Set<Company>()
        .FirstOrDefaultAsync(c => c.Id == companyId);
    if (company is null)
    {
        return Results.NotFound($"The Company with Id '{companyId}' was not found");
    }
    await dbContext.Database.BeginTransactionAsync();
    await dbContext.Database.ExecuteSqlInterpolatedAsync($"UPDATE Employees Set Salary = Salary*1.1 WHERE CompanyId= {companyId}");
    company.LastSalaryUpdateUtc = DateTime.UtcNow;
    await dbContext.SaveChangesAsync();
    await dbContext.Database.CommitTransactionAsync();

    return Results.NoContent();
});


app.MapPut("increase-salaries-sql-dapper", async (int companyId, DatabaseContext dbContext) => {

    var company = await dbContext.Set<Company>()
        .FirstOrDefaultAsync(c => c.Id == companyId);
    if (company is null)
    {
        return Results.NotFound($"The Company with Id '{companyId}' was not found");
    }
    var transaction = await dbContext.Database.BeginTransactionAsync();
    await dbContext.Database.GetDbConnection().ExecuteAsync(
        "UPDATE Employees Set Salary = Salary*1.1 WHERE CompanyId= @CompanyId",
        new {ComPanyId=companyId},transaction.GetDbTransaction());


    company.LastSalaryUpdateUtc = DateTime.UtcNow;
    await dbContext.SaveChangesAsync();
    await dbContext.Database.CommitTransactionAsync();

    return Results.NoContent();
});
app.Run();
