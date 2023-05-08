using Serilog;
using Serilog.Events;
using System.Reflection;
using VKInternshipTask.Application.Common.Interfaces;
using VKInternshipTask.Application.Common.Mappings;
using VKInternshipTask.Application.Extensions;
using VKInternshipTask.Persistence;
using VKInternshipTask.Persistence.Extensions;
using VKInternshipTask.WebApi.Extensions;
using VKInternshipTask.WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .WriteTo.File("VKInternshipTask-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(IUsersAPIDbContext).Assembly));
});
builder.Services.AddAuthentication("Basic")
    .AddBasicAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("InitialPolicy", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
    });
});

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

app.UseCustomExceptionHandler();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("InitialPolicy");
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
