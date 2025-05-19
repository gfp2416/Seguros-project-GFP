using MediatR;
using Microsoft.EntityFrameworkCore;
using Seguros.Application.Commands.CreatePersona;
using Seguros.Application.Services;
using Seguros.Domain.Interfaces.Repositories;
using Seguros.Infrastructure.Repositories;
using Seguros.Persistence;
using Serilog;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Seguros.Domain.Entities;
using Microsoft.Extensions.Logging;
using Serilog.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// EF Core
builder.Services.AddDbContext<SegurosDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

// Repository
builder.Services.AddScoped<IPersonaRepository, PersonaRepository>();

// MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(CreatePersonaCommand).Assembly));

// Logging
using var loggerFactory = LoggerFactory.Create(loggingBuilder => loggingBuilder
    .SetMinimumLevel(LogLevel.Trace)
    .AddConsole());

var logger = loggerFactory.CreateLogger<Program>();

// Authentication

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

// CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy.WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userRepo = services.GetRequiredService<IUsuarioRepository>();
    var authService = services.GetRequiredService<IAuthService>();

    var adminEmail = "admin@ficticia.com";
    var existingAdmin = await userRepo.GetByEmailAsync(adminEmail);

    if (existingAdmin == null)
    {
        var admin = new Usuario
        {
            NombreUsuario = "admin",
            Email = adminEmail,
            Rol = "Admin",
            PasswordHash = authService.HashPassword("admin1234") // contraseña segura
        };

        await userRepo.AddAsync(admin);
        logger.LogInformation("Usuario administrador creado automáticamente.");
    }
    else
    {
        logger.LogInformation("Usuario administrador ya existía.");
    }
}

app.Run();
