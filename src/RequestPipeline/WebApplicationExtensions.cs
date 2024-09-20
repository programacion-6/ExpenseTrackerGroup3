using ExpenseTrackerGroup3.Data;

namespace ExpenseTrackerGroup3.RequestPipeline;

public static class WebApplicationExtensions
{
    public static WebApplication InitializeDatabase(this WebApplication app)
    {
        var dbConnection = app.Services.GetRequiredService<IDbConnectionFactory>();

        using var connection = dbConnection.CreateConnectionAsync().Result;

        var connectionString = connection.ConnectionString;

        DbInitializer.Initializer(connectionString);

        return app;
    }
}