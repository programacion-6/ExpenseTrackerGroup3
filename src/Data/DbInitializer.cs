namespace ExpenseTrackerGroup3.Data;

using System.Reflection;

using DbUp;

public class DbInitializer
{
    public static void Initializer(string connectionString)
    {
        EnsureDatabase.For.PostgresqlDatabase(connectionString);

        var dpUp = DeployChanges.To.PostgresqlDatabase(connectionString)
        .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
        .WithTransaction()
        .LogToConsole()
        .Build();

        var result = dpUp.PerformUpgrade();

        if (!result.Successful)
        {
            Console.WriteLine("Invalid Migrations");
        }
    }
}