using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PortalGalaxy.Common;
using PortalGalaxy.DataAccess;
using PortalGalaxy.Repositories.Implementaciones;
using PortalGalaxy.Repositories.Interfaces;
using PortalGalaxy.Services.Implementaciones;
using PortalGalaxy.Services.Interfaces;
using PortalGalaxy.Services.Profiles;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//var logger = new LoggerConfiguration()
//    .WriteTo.Console()
//    .CreateLogger();

//builder.Logging.ClearProviders();
//builder.Logging.AddSerilog(logger);

// Vamos a leer el archivo de configuracion con una clase mapeada
builder.Services.Configure<AppConfiguration>(builder.Configuration);

// Add services to the container.

builder.Services.AddDbContext<PortalGalaxyDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("GalaxyDatabase"));
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApplicationDb"));
});

builder.Services.AddIdentity<GalaxyIdentityUser, IdentityRole>(policies =>
{
    // Politicas de contraseña
    policies.Password.RequireDigit = true;
    policies.Password.RequireLowercase = true;
    policies.Password.RequireUppercase = false;
    policies.Password.RequireNonAlphanumeric = false;
    policies.Password.RequiredLength = 6;

    // Los usuarios deben ser unicos
    policies.User.RequireUniqueEmail = true;

    // Politica de bloqueo de cuentas
    policies.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
    policies.Lockout.MaxFailedAccessAttempts = 5;
}).AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();


// Inyectamos las dependencias

// AutoMapper

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<PortalGalaxyProfile>();
});

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ICategoriaService, CategoriaService>();
builder.Services.AddTransient<IAlumnoService, AlumnoService>();
builder.Services.AddTransient<ITallerService, TallerService>();

builder.Services.AddTransient<IAlumnoRepository, AlumnoRepository>();
builder.Services.AddTransient<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddTransient<ITallerRepository, TallerRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    var key = Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("Jwt:SecretKey") ??
        throw new InvalidOperationException("No se configuro el JWT"));

    x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Emisor"],
        ValidAudience = builder.Configuration["Jwt:Audiencia"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Validacion de usuarios y passwords
app.UseAuthentication();
// Validacion de permisos
app.UseAuthorization();

app.MapGet("api/Categorias", async (ICategoriaService service) =>
{
    return Results.Ok(await service.ListAsync());
});

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    await UserDataSeeder.Seed(scope.ServiceProvider);
}

app.Run();
