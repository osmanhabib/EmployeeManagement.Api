using EmployeeManagement.Data;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.DependencyInjection;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console() 
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .WriteTo.Seq("http://localhost:5107")
    .CreateLogger();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<EmployeeManagementDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeManagementDBConnectionString")));

builder.Services.AddDependency(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler("/error");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
