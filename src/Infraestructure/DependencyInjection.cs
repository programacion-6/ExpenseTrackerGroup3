using Domain.DTOs;
using ExpenseTrackerGroup3.Data;
using ExpenseTrackerGroup3.Data.Concretes;
using ExpenseTrackerGroup3.Data.Interfaces;
using ExpenseTrackerGroup3.Middleware;
using ExpenseTrackerGroup3.Domain.DTOs;
using ExpenseTrackerGroup3.Repositories;
using ExpenseTrackerGroup3.Repositories.Interfaces;
using ExpenseTrackerGroup3.Services;
using ExpenseTrackerGroup3.Services.Interfaces;
using ExpenseTrackerGroup3.Utils;
using ExpenseTrackerGroup3.Utils.EmailSender;
using ExpenseTrackerGroup3.Utils.Hasher.Interfaces;
using ExpenseTrackerGroup3.Utils.Jwt;
using ExpenseTrackerGroup3.Utils.Jwt.Interfaces;
using ExpenseTrackerGroup3.Utils.NotifyMilestone;
using ExpenseTrackerGroup3.Utils.NotifyMilestone.Interfaces;
using ExpenseTrackerGroup3.Utils.Swagger;
using ExpenseTrackerGroup3.Validators.AuthValidator;
using ExpenseTrackerGroup3.Validators.BudgetValidator;
using ExpenseTrackerGroup3.Validators.ExpenseValidator;
using ExpenseTrackerGroup3.Validators.GoalValidator;
using ExpenseTrackerGroup3.Validators.IncomeValidator;
using ExpenseTrackerGroup3.Validators.UserValidator;

using FluentValidation;

namespace ExpenseTrackerGroup3.Infraestructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDatabase(configuration)
            .AddAuthServices()
            .AddSwaggerAuthentication()
            .AddRepositories()
            .AddServices()
            .AddJwtAuthentication(configuration)
            .AddValidators();
        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DatabaseOptions>(configuration.GetSection(DatabaseOptions.ConnectionStrings));
        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.Jwt));
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
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IExpenseService, ExpenseService>();
        services.AddScoped<IGoalService, GoalService>();
        services.AddScoped<IIncomeService, IncomeService>();
        services.AddScoped<IGoalNotifyService, GoalNotifyService>();
        return services;
    }

    private static IServiceCollection AddAuthServices(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddTransient<IEmailSender, EmailSender>();
        services.AddSingleton<SmtpOptions>();
        return services;
    }

    private static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<LoginRequest>, LoginValidator>();
        services.AddScoped<IValidator<CreateUser>, RegisterValidator>();
        services.AddScoped<IValidator<RequestResetPassword>, EmailValidator>();
        services.AddScoped<IValidator<ResetPassword>, PasswordValidator>();
        services.AddScoped<IValidator<CreateBudget>, BudgetValidator>();
        services.AddScoped<IValidator<CreateExpense>, ExpenseValidator>();
        services.AddScoped<IValidator<CreateGoal>, GoalValidator>();
        services.AddScoped<IValidator<CreateIncome>, IncomeValidator>();
        services.AddScoped<IValidator<UpdateUserDTO>, UserValidator>();
        return services;
    }
}
