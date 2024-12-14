using EmployeeManagement.Data;
using EmployeeManagement.Domain.Entities;
using EmployeeManagement.Extensions;
using EmployeeManagement.Features.Employees.Commands;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EmployeeManagement.Features.Employees.Handlers;

public class CreateEmployeeCommandHandler(IValidator<CreateEmployeeCommand> validator,
    EmployeeManagementDbContext context)
{
    private Employee Handle(CreateEmployeeCommand command)
    {
        return HandleAsync(command).Result;
    }

    public async Task<Employee> HandleAsync(CreateEmployeeCommand command)
    {
        var validationResult = await validator.ValidateAsync(command);
        validationResult.EnsureValidResult();

        var existingUser = await context.Users.FirstOrDefaultAsync(u => u.UserName == command.Email);

        if (existingUser is not null)
        {
            throw new DataException("User is already exists");
        };
        var user = new User()
        {
            UserName = command.Email,
            Password = command.Password,
            CreatedBy = command.CreatedBy,
            Role = "user"
        };

        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();

        if(user is not null && user.UserId > 0)
        {

        };


        return new Employee();
    }

}
