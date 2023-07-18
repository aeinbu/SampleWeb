var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IMyTransientService, MyService>();
builder.Services.AddScoped<IMyScopedService, MyService>();
builder.Services.AddSingleton<IMySingletonService, MyService>();

var app = builder.Build();


app.UseHttpLogging();
//TODO:
// v configuration
// - environment vars
// - secrets
// v logging
// - Serilog
// v DI
// - Autofac
// * authentication
// - authorization
// - SignalR

app.Logger.LogInformation("The app started");
app.MapGet("/", () => "Hello World!");

app.MapGet("/config/", () => $"""
    The config settings are:
        - {app.Configuration["config_a"]}
        - {app.Configuration["config_b"]}
        - {app.Configuration["config_c"]}
    """);


app.MapGet("/scopes", () => {
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

app.Run();
