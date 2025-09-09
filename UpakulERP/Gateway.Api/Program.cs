using Gateway.Api.Extensions;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Values;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        policy => { policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin(); });
});
builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    // Multi Dynamic File Write
    .AddOcelotConfigFiles($"./{builder.Environment.EnvironmentName}", new[] { "account", "auth", "fixedAsset", "gb", "hrm","mf", "project" }, builder.Environment);
    
    // For Single file read
    //.AddJsonFile($"service.{builder.Environment.EnvironmentName}.json",optional:false,reloadOnChange:true)

builder.Services.AddOcelot(builder.Configuration)
    .AddCacheManager(o=>o.WithDictionaryHandle());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  //  app.MapOpenApi();
}
app.UseRouting();
app.UseCors("CorsPolicy");
await app.UseOcelot();
await app.RunAsync();
