using Microsoft.OpenApi.Models;
using EscolaPro.Extensions;
using Microsoft.EntityFrameworkCore.Internal;
using EscolaPro.Database;
using Microsoft.EntityFrameworkCore;
using EscolaPro.Database.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


//----------------------------- Config Swagger -----------------------------
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo() { Title = "aplicatalogo", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Bearer JWT ",
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
            new string[]{}
        }
    });
});

//----------------------------- Database -----------------------------
builder.Services.AddDbContext<GeneralDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("General")));

builder.Services.AddScoped<IAppDbContextFactory, AppDbContextFactory>();

//----------------------------- Cors -----------------------------
var OriginsWithAllowedAccess = "OriginsWithAllowedAccess";

builder.Services.AddCors(options =>
    options.AddPolicy(name: OriginsWithAllowedAccess,
        policy =>
        {
            // Especifique as origens permitidas explicitamente
            policy.WithOrigins("http://192.168.1.65:4200", "http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        })
);

// -------------------------- Configuring AutoMapper --------------------------
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// --------- Active the HttpResponseException middleware ------------
app.ConfigureExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(OriginsWithAllowedAccess);
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
