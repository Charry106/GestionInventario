
using GestionInventario.Datos;
using GestionInventario.Datos.Repositorio;
using GestionInventario.Datos.Negocio.Servicios;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Cargar la configuración de conexión desde appsettings.Bd.json
builder.Configuration.AddJsonFile("appsettings.Bd.json", optional: false, reloadOnChange: true);

// Configurar Entity Framework Core con MySQL para GestionInventarioDbContext
builder.Services.AddDbContext<GestionInventarioDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurar Identity con la clase personalizada Usuario y requisitos de contraseña relajados
builder.Services.AddIdentity<Usuario, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 4;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
})
.AddEntityFrameworkStores<GestionInventarioDbContext>()
.AddDefaultTokenProviders();

// Configuración de servicios y repositorios como Scoped
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IUsuarioServicio, UsuarioServicio>();
builder.Services.AddScoped<IProductoRepositorio, ProductoRepositorio>();
builder.Services.AddScoped<IProveedorRepositorio, ProveedorRepositorio>();
builder.Services.AddScoped<IMovimientoRepositorio, MovimientoRepositorio>(); 
builder.Services.AddScoped<IMovimientoServicio, MovimientoServicio>();
builder.Services.AddScoped<IProductoServicio, ProductoServicio>();
builder.Services.AddScoped<IProveedorServicio, ProveedorServicio>();

// Configuración de controladores y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware de Swagger y desarrollo
if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Bd"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();