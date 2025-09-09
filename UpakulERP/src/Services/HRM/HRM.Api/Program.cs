using HRM.Application.Extensions;
using HRM.Infrastructure.Extensions;
using MessageBroker.Services.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Utility.Security;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddApiVersioning();
builder.Services.AddEndpointsApiExplorer();
/// Dependency Inject
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddMQInfrastructureServices(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
        //.WithOrigins("http://localhost:4200/")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        /*.WithExposedHeaders("*")*/);
});

builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
        .AddCookie(cookie =>
        {
            cookie.AccessDeniedPath = "/Account/logout";
            // cookie.SlidingExpiration = true;
        })
        .AddJwtBearer(jwt =>
        {
            jwt.Audience = JwtSettings.Issuer;
            jwt.Authority = JwtSettings.Audience;
            jwt.RequireHttpsMetadata = false;
            //jwt.SaveToken = true;
            jwt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = JwtSettings.Issuer,
                ValidAudience = JwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtSettings.SecretKey)),
                ValidateIssuer = true,
                RequireAudience = true,
                RequireExpirationTime = true,
                RequireSignedTokens = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidateTokenReplay = false,
                ValidateActor = false,
                ValidateAudience = true,
                ClockSkew = TimeSpan.Zero
            };
            jwt.Configuration = new OpenIdConnectConfiguration();

        });

var app = builder.Build();

// Enable CORS
app.UseCors("AllowAll");
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, builder.Configuration.GetValue<string>("FileStorageLocation"))),
    RequestPath = "/Resources"
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    app.MapOpenApi();

app.UseAuthorization();
app.MapControllers();
await app.RunAsync();
