using MessageBroker.Consumer.Extensions;
using MessageBroker.Services.Extensions;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddMQInfrastructureServices(builder.Configuration);
var host = builder.Build();
var loggerFactory = host.Services.GetService<ILoggerFactory>();
loggerFactory.AddFile(builder.Configuration["Logging:LogFilePath"].ToString());

//host.Run();
await host.RunAsync();
