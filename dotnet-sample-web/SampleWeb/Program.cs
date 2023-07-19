using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IMyTransientService, MyService>();
builder.Services.AddScoped<IMyScopedService, MyService>();
builder.Services.AddSingleton<IMySingletonService, MyService>();

builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorizationBuilder()
  .AddPolicy("secrets-admin", policy =>
        policy
            .RequireRole("admin")
            .RequireClaim("scope", "secrets-api"));

builder.Services.AddControllers();

var app = builder.Build();
app.Logger.LogInformation("The app started");

app.UseHttpLogging();

//TODO:
// v configuration
// - environment vars
// - secrets
// v logging
// - Serilog
// v DI
// - Autofac
// v authentication
// v authorization
// - SignalR

app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.MapGet("/config/", () => $"""
    The config settings are:
        - {app.Configuration["config_a"]}
        - {app.Configuration["config_b"]}
        - {app.Configuration["config_c"]}
    """);


app.MapGet("/scopes", () =>
{
    using var scope = app.Services.CreateScope();
    return $"""
        The scopes are:
            - IMyScopedService {scope.ServiceProvider.GetRequiredService<IMyScopedService>()}
            - IMyScopedService {scope.ServiceProvider.GetRequiredService<IMyScopedService>()}
            - IMySingletonService {scope.ServiceProvider.GetRequiredService<IMySingletonService>()}
            - IMySingletonService {scope.ServiceProvider.GetRequiredService<IMySingletonService>()}
            - IMyTransientService {scope.ServiceProvider.GetRequiredService<IMyTransientService>()}
            - IMyTransientService {scope.ServiceProvider.GetRequiredService<IMyTransientService>()}
            - new MyService() {new MyService()}
            - new MyService() {new MyService()}
        """;
});

app.MapGet("/secret", [Authorize](ClaimsPrincipal user) => $"For your eyes only, {user.Identity?.Name} - You are authorized to see this");

app.MapGet("/secret-admin", [Authorize("secrets-admin")](ClaimsPrincipal user) => $"You are a secrets-admin if you're seeing this");

app.Run();
