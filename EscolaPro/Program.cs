using Microsoft.OpenApi.Models;
using EscolaPro.Extensions;
using EscolaPro.Database;
using Microsoft.EntityFrameworkCore;
using EscolaPro.Database.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using EscolaPro.Repositories.Interfaces;
using EscolaPro.Repositories;
using EscolaPro.Services.Interfaces;
using EscolaPro.Services;
using Microsoft.Extensions.FileProviders;

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

// -------------------- Autentica��o e autoriza��o ---------------------
var secretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");
var issuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
var audience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");

if (string.IsNullOrEmpty(secretKey) || string.IsNullOrEmpty(issuer) || string.IsNullOrEmpty(audience))
{
    throw new InvalidOperationException("Algumas variáveis de ambiente não foram configuradas corretamente.");
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            ValidIssuer = issuer,
            ValidAudience = audience,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
        };

        // // Permite a configuração de um evento para quando o token for validado
        // options.Events = new JwtBearerEvents
        // {
        //     OnAuthenticationFailed = context =>
        //     {
        //         // Se falhar a autenticação, você pode logar a falha ou fazer algo
        //         Console.WriteLine("Falha na autenticação: " + context.Exception.Message);
        //         return Task.CompletedTask;
        //     },
        //     OnTokenValidated = context =>
        //     {
        //         // Se o token for validado, você pode fazer algo, como logar ou adicionar dados ao contexto
        //         Console.WriteLine("Token validado!");
        //         return Task.CompletedTask;
        //     }
        // };
    });

// Adiciona a autorização
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("admin", policy =>
        policy.RequireAssertion(context =>
            context.User.HasClaim("Role", "admin") &&
            context.User.HasClaim("Role", "moderador") &&
            context.User.HasClaim("Role", "user")));

    options.AddPolicy("moderador", policy =>
        policy.RequireAssertion(context =>
            context.User.HasClaim("Role", "moderador") &&
            context.User.HasClaim("Role", "user")));

    options.AddPolicy("user", policy => 
        policy.RequireClaim("Role", "user"));

    options.AddPolicy("admin_internal", policy =>
        policy.RequireClaim("Role", "admin_internal"));
});

// Adicionando políticas globais
builder.Services.AddMvc(config =>
{
    var policy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    config.Filters.Add(new AuthorizeFilter(policy));
}).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

//----------------------------- Database -----------------------------
builder.Services.AddDbContext<GeneralDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("General")));


// ----------------------- Inject container ------------------------------
//builder.Services.AddScoped<IUsersRepository, UsersRepository>();
//builder.Services.AddScoped<ISectorRepository, SectorRepository>();
//builder.Services.AddScoped<ICallRepository, CallRepository>();
builder.Services.AddScoped<ISaltRepository, SaltRepository>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IUsersGeneralRepository, UsersGeneralRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ICompanieRepository, CompanieRepository>();
builder.Services.AddScoped<IFamilyRepository, FamilyRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IAllergieRepository, AllergieRepository>();
builder.Services.AddScoped<IDatabaseService, DatabaseService>();
builder.Services.AddScoped<IAppDbContextFactory, AppDbContextFactory>();
builder.Services.AddScoped<IImageService, ImageService>();

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

// -------------------------- Configuring StaticFiles --------------------------

app.UseStaticFiles(); // já serve wwwroot

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "uploads")),
    RequestPath = "/uploads"
});

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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
