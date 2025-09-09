using System.Text;
using Auth.API.Context;
using Auth.API.Extensions;
using Auth.API.Models;
using Auth.API.Services;
using Message.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Utility.Security;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddInfrastructureModule();

// Identity DB Context
var connectionString = builder.Configuration.GetConnectionString("AuthConnection");
builder.Services.AddDbContext<AppDbContext>(cfg => cfg.UseSqlServer(connectionString));
var mf_connectionString = builder.Configuration.GetConnectionString("MFConnection");
builder.Services.AddDbContext<MFDbContext>(cfg => cfg.UseSqlServer(mf_connectionString));
builder.Services.AddMessageInfrastructureServices(builder.Configuration);
builder.Services.AddApiVersioning();
builder.Services.AddEndpointsApiExplorer();

// Identity AddIdentityCore
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.User.RequireUniqueEmail = false;
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789@.";
    options.Password = new PasswordOptions
    {
        RequiredLength = 5,
        RequireUppercase = true,
        RequireLowercase = true,
        RequireNonAlphanumeric = true,
        RequireDigit = true,
    };
}).AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

//var key = Encoding.ASCII.GetBytes(secretKey!);
var tempProvider = builder.Services.BuildServiceProvider();
var jsonDataGenerate = tempProvider.GetRequiredService<IJsonDataGenerate>();
string secretKey = await jsonDataGenerate.LoadJwtSettingsFromFileAsync(1);
builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
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
            jwt.SaveToken = true;
            jwt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = JwtSettings.Issuer,
                ValidAudience = JwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey/*JwtSettings.SecretKey*/)),
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


var CorsPolicy = "CorsPolicy";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: CorsPolicy,
                      policy =>
                      {
                          policy.WithOrigins("*")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

var app = builder.Build();

//using var scope = app.Services.CreateScope();
//var dg = scope.ServiceProvider.GetRequiredService<IJsonDataGenerate>();
//await dg.WriteJsonAsync();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("CorsPolicy");


//app.Run();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
//app.Run();
await app.RunAsync();