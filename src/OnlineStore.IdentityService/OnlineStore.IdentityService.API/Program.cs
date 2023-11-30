using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OnlineStore.IdentityService.API.Extensions;
using OnlineStore.IdentityService.API.Middlewares;
using OnlineStore.IdentityService.BLL.Settings;
using OnlineStore.IdentityService.DAL.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

builder.Services.Configure<JWTSettings>(configuration.GetSection(JWTSettings.SectionName));

// Register services
builder.Services.AddServicesRegistration();
builder.Services.AddValidatorsRegistration();
builder.Services.AddDbContextRegistration(configuration);
builder.Services.AddJwtAuthentication(configuration);

builder.Services.AddTransient<ExceptionHandlingMiddleware>();

builder.Services.AddRouting(options => 
{
    options.LowercaseUrls = true;
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.InitializeDb();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
