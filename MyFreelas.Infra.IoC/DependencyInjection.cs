using FluentValidation;
using HashidsNet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyFreelas.Application.Dtos.Customer;
using MyFreelas.Application.Dtos.Dashboard;
using MyFreelas.Application.Dtos.Freela;
using MyFreelas.Application.Dtos.User;
using MyFreelas.Application.Interfaces;
using MyFreelas.Application.Mapper;
using MyFreelas.Application.Services;
using MyFreelas.Application.Validators;
using MyFreelas.Domain.Interfaces;
using MyFreelas.Infra.Data.Context;
using MyFreelas.Infra.Data.Repositories;

namespace MyFreelas.Infra.IoC;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, 
        IConfiguration configuration)
    {
        AddContext(services, configuration); 
        AddRepositories(services);
        AddServices(services);
        AddValidators(services);
        AddMapper(services);
        AddHashIds(services, configuration);
    }

    private static void AddContext(this IServiceCollection services, IConfiguration configuration)
    {
         services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(
                    configuration.GetConnectionString("DefaultConnection"),
                    ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection")),
                    b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
                )
            );
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IFreelaRepository, FreelaRepository>();
        services.AddScoped<IDashboardRepository, DashboardRepository>();
        services.AddScoped<IInstallmentRepository, InstallmentRepository>();
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ILoginService, LoginService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IFreelaService, FreelaService>();
        services.AddScoped<IDashboardService, DashboardService>();
        services.AddScoped<ILoggedService, LoggedService>();
        services.AddScoped<IHashidService, HashidService>();
        services.AddHttpContextAccessor();
    }

    private static void AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<RequestRegisterUserJson>, RegisterUserValidator>();
        services.AddScoped<IValidator<RequestAuthenticationJson>, AuthenticationValidator>();
        services.AddScoped<IValidator<RequestUpdatePasswordJson>, UpdatePasswordValidator>();
        services.AddScoped<IValidator<RequestCustomerJson>, RegisterCustomerValidator>();
        services.AddScoped<IValidator<RequestRegisterFreelaJson>, RegisterFreelaValidator>();
        services.AddScoped<IValidator<RequestUpdateFreelaJson>, UpdateFreelaValidator>();
        services.AddScoped<IValidator<RequestGetMonthlyBillingJson>, GetMonthlyBillingValidator>();
    }

    private static void AddMapper(this IServiceCollection services)
    {
        services.AddScoped(provider => new AutoMapper.MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile(provider.GetService<IHashids>()));
        }).CreateMapper());
    }

    private static void AddHashIds(this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddScoped<IHashids>(_ =>
            new Hashids(configuration.GetValue<string>("HashIds:Salt"), 3)
        );
    }
}
