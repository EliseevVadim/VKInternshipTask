using Serilog;
using Serilog.Events;
using VKInternshipTask.Persistence;
using VKInternshipTask.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .WriteTo.File("VKInternshipTask-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Services.AddPersistence(builder.Configuration);

var app = builder.Build();

using (var scopes = app.Services.CreateScope())
{
    try
    {
        var serviceProvider = scopes.ServiceProvider;
        UsersAPIDbContext context = serviceProvider.GetRequiredService<UsersAPIDbContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception exception)
    {
        Log.Fatal(exception, "An error occurred at app initialization");
    }
}


app.MapGet("/", () => "Hello World!");

app.Run();
