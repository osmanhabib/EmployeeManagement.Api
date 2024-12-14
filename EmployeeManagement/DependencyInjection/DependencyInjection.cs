using EmployeeManagement.Authentication;
using EmployeeManagement.Features.Employees.Handlers;
using EmployeeManagement.Features.Employees.Validators;
using EmployeeManagement.Services;
using EmployeeManagement.Services.Implementations;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace EmployeeManagement.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddDependency(this IServiceCollection services,
        ConfigurationManager configurationManager)
    {
        services.Configure<JwtSettings>(configurationManager.GetSection(JwtSettings.SectionName));

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true, // Validate the issuer
                    ValidateAudience = false, // Validate the audience
                    ValidateLifetime = true, // Validate the expiration of the token
                    ValidateIssuerSigningKey = true, // Validate the signing key
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("F9E9006E-1977-4575-B8A6-7ED4C0A7D36A")), // Secret key to validate the signature
                    ValidIssuer = "EmployeeManagement", // Valid issuer
                    ValidAudience = "EmployeeManagement", // Valid audience
                };

                // Optional: You can handle the token validation events to add custom logic
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        // Add custom logic after token is validated (e.g., logging, user info retrieval)
                        var claimsIdentity = (ClaimsIdentity)context.Principal?.Identity;
                        var userId = claimsIdentity?.FindFirst("userId")?.Value;

                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        // Handle authentication failures (e.g., log errors)
                        if (context.Exception is SecurityTokenExpiredException)
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }

                        return Task.CompletedTask;
                    }
                };
            });

        services.AddControllers()
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateEmployeeCommandValidator>());

        services.AddScoped<CreateEmployeeCommandHandler>();

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

        return services;
    }
}
