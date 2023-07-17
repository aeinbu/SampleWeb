var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//TODO:
// * configuration
// - environment vars
// - secrets
// * logging
// - Serilog
// * DI
// - Autofac
// * authentication
// - authorization
// - SignalR

app.MapGet("/", () => "Hello World!");

app.Run();
