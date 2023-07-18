var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//TODO:
// v configuration
// - environment vars
// - secrets
// - logging
// - Serilog
// * DI
// - Autofac
// * authentication
// - authorization
// - SignalR

app.MapGet("/", () => "Hello World!");

app.MapGet("/config1/", () => $"The config settings are:\r\n- {app.Configuration["config_a"]}\r\n- {app.Configuration["config_b"]}\r\n- {app.Configuration["config_c"]}");

app.Run();
