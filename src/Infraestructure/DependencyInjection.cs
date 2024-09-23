using ExpenseTrackerGroup3.Data;
using ExpenseTrackerGroup3.Data.Concretes;
using ExpenseTrackerGroup3.Data.Interfaces;
using ExpenseTrackerGroup3.Repositories;
using ExpenseTrackerGroup3.Repositories.Interfaces;
using ExpenseTrackerGroup3.Services;
using ExpenseTrackerGroup3.Services.Interfaces;

namespace ExpenseTrackerGroup3.Infraestructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.
            AddDatabase(configuration)
            .AddRepositories()
            .AddServices();
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
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IBudgetRepository, BudgetRepository>();
        services.AddScoped<IExpenseRepository, ExpenseRepository>();
        services.AddScoped<IGoalRepository, GoalRepository>();
        services.AddScoped<IIncomeRepository, IncomeRepository>();
        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IBudgetService, BudgetService>();
        return services;
    }
}
