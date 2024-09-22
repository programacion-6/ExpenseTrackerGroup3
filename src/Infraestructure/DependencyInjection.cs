using ExpenseTrackerGroup3.Data;
using ExpenseTrackerGroup3.Data.Concretes;
using ExpenseTrackerGroup3.Data.Interfaces;
using ExpenseTrackerGroup3.Repositories;
using ExpenseTrackerGroup3.Repositories.Interfaces;

namespace ExpenseTrackerGroup3.Infraestructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.
            AddDatabase(configuration)
            .AddRepositories();
        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseOptions>(configuration.GetSection(DatabaseOptions.ConnectionStrings));
        services.AddScoped<IDbConnectionFactory, DbConnection>();
        services.AddScoped<IDbInitializer, DbInitializer>();
        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        // Here ned to be others repositories for inyection 
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IBudgetRepository, BudgetRepository>();
        services.AddScoped<IExpenseRepository, ExpenseRepository>();

        return services;
    }
}
