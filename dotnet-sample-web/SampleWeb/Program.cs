var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseHttpLogging();
//TODO:
// v configuration
// - environment vars
// - secrets
// v logging
// - Serilog
// * DI
// - Autofac
// * authentication
// - authorization
// - SignalR

app.Logger.LogInformation("The app started");
app.MapGet("/", () => "Hello World!");

app.MapGet("/config1/", () => $"The config settings are:\r\n- {app.Configuration["config_a"]}\r\n- {app.Configuration["config_b"]}\r\n- {app.Configuration["config_c"]}");

app.Run();
