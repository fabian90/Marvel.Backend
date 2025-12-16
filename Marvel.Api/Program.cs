using Marvel.Application.Interfaces;
using Marvel.Application.Services;
using Marvel.Application.Validators.Auth;
using Marvel.Application.Validators.Marvel;
using Marvel.Domain.Interfaces;
using Marvel.Domain.Repositories;
using Marvel.Infrastructure.Authentication;
using Marvel.Infrastructure.Persistence;
using Marvel.Infrastructure.Repositories;
using Marvel.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Text;
using Marvel.Application.Queries.Marvel;
using Marvel.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

#region Controllers y Validaciones

// Registro de controladores
builder.Services.AddControllers();

#region Cache
builder.Services.AddMemoryCache();
#endregion

// FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

// Registro automático de validadores
builder.Services.AddValidatorsFromAssemblyContaining<RegisterRequestDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<GetComicsQueryValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<AddFavoriteRequestValidator>();

#endregion

#region CORS

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                 .SetPreflightMaxAge(TimeSpan.FromMinutes(30));
        });
});

#endregion

#region Swagger (Documentación API)

// Explorador de endpoints
builder.Services.AddEndpointsApiExplorer();

// Configuración Swagger + JWT
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Marvel Backend API",
        Version = "v1",
        Description = "API Backend con autenticación JWT y consumo de PokeAPI v2."
    });

    // Configuración de seguridad JWT Bearer
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description =
            "Autorización JWT usando el esquema Bearer.\n\n" +
            "Ingrese el token con el prefijo 'Bearer '.\n\n" +
            "Ejemplo: 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

    // Habilitar comentarios XML
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

#endregion

#region Base de Datos (Infraestructura)

// Base de datos en memoria (prueba técnica)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("MarvelDb"));

#endregion

#region Infraestructura - Seguridad y Persistencia

// Autenticación y seguridad
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();

// Repositorio de favoritos Pokémon
builder.Services.AddScoped<IPokemonFavoriteRepository, PokemonFavoriteRepository>();

#endregion

#region Capa Application - Servicios

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IFavoriteService, FavoriteService>();

#endregion

#region MediatR (CQRS)

// Registro de handlers CQRS
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblyContaining<GetPokemonsQuery>());

#endregion

#region HttpClient - PokeAPI

// Cliente HTTP para consumo de PokeAPI v2
builder.Services.AddHttpClient<IPokeApiService, PokeApiService>();

#endregion

#region JWT Configuration

var secretKey = builder.Configuration["Jwt:Secret"];
var key = Encoding.UTF8.GetBytes(secretKey!);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

#endregion

var app = builder.Build();

#region Pipeline HTTP

// Swagger solo en entorno desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// CORS siempre antes de Auth
app.UseCors("AllowAngular");
// Middleware de autenticación y autorización
app.UseAuthentication();
app.UseAuthorization();

// Mapear controladores
app.MapControllers();

#endregion

app.Run();
