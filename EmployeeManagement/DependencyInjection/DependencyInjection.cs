using EmployeeManagement.Features.Employees.Handlers;
using EmployeeManagement.Features.Employees.Validators;
using EmployeeManagement.Services;
using EmployeeManagement.Services.Implementations;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using EmployeeManagement.Data;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Utilities;
using EmployeeManagement.Features.Department.Handlers;
using EmployeeManagement.Features.Designation.Handlers;
using FluentValidation;

namespace EmployeeManagement.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddDependency(this IServiceCollection services,
        ConfigurationManager configurationManager)
    {
        services.Configure<JwtSettings>(configurationManager.GetSection(JwtSettings.SectionName));

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();

        services.AddDbContext<EmployeeManagementDbContext>(options =>
            options.UseSqlServer(configurationManager.GetConnectionString("EmployeeManagementDBConnectionString")));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes("F9E9006E-1977-4575-B8A6-7ED4C0A7D36A")),
                    ValidIssuer = "EmployeeManagement", // Valid issuer
                    ValidAudience = "EmployeeManagement", // Valid audience
                };

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception is SecurityTokenExpiredException)
                        {
                            context.Response.Headers.Append("Token-Expired", "true");
                        }

                        return Task.CompletedTask;
                    }
                };
            });

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<CreateEmployeeCommandValidator>();

        services.AddScoped<CreateEmployeeCommandHandler>();
        services.AddScoped<GetEmployeeQueryHandler>();
        services.AddScoped<UpdateEmployeeCommandHandler>();
        services.AddScoped<DeleteEmployeeCommandHandler>();

        services.AddScoped<GetDepartmentsQueryHandler>();
        services.AddScoped<GetDesignationsQueryHandler>();

        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);

            options.AssumeDefaultVersionWhenUnspecified = true;

            options.ReportApiVersions = true;

            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),  // Version in URL (e.g., /api/v1/controller)
                new HeaderApiVersionReader("x-api-version")  // Version in custom header (e.g., x-api-version: 1)
            );
        });

        services.AddHttpContextAccessor();

        return services;
    }
}
