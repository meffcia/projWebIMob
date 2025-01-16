using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OnlineShop.Api.Data;
using OnlineShop.Api.Services;
using OnlineShop.Shared.Services.ProductService;
using OnlineShop.Shared.Services.CategoryService;
using OnlineShop.Shared.Services.CartService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<IProductService, OnlineShop.Api.Services.ProductService>();
builder.Services.AddScoped<ICategoryService, OnlineShop.Api.Services.CategoryService>();
builder.Services.AddScoped<OnlineShop.Api.Services.IOrderService, OnlineShop.Api.Services.OrderService>();
builder.Services.AddScoped<ICartService, OnlineShop.Api.Services.CartService>();
builder.Services.AddScoped<IAuthService, AuthService>();

string token = builder.Configuration.GetSection("AppSettings:Token").Value;
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(token)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
{
#if WINDOWS
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
#elif MACOS
    options.UseSqlite(builder.Configuration.GetConnectionString("SqlDB"));
#else
    throw new PlatformNotSupportedException("This platform is not supported for database configuration.");
#endif
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", policy =>
    {
        policy.WithOrigins("http://localhost:5068") // Adres Maui
              .AllowAnyHeader()
              .AllowAnyMethod();
    });

    options.AddPolicy("AllowWPFClient", policy =>
    {
        policy.WithOrigins("http://localhost:5067") // Adres aplikacji WPF
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowBlazorClient");

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();