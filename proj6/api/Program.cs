using Microsoft.EntityFrameworkCore;
using proj6.Data;
using proj6.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add database service (Entity Framework with SQL Server)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy => policy.WithOrigins("http://localhost:3000") // Replace with your frontend's URL
                        .AllowCredentials() // Allow credentials
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

// Add SignalR service
builder.Services.AddSignalR();
builder.Logging.AddConsole().SetMinimumLevel(LogLevel.Debug);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// Configure the HTTP request pipeline.
app.UseStaticFiles();


// Enable CORS policy globally
// app.UseCors();//"AllowAll");
app.UseCors("AllowSpecificOrigin");

app.UseRouting();

// app.UseAuthorization();

// Map controllers
app.MapControllers();

// Mapuj SignalR Hub
app.MapHub<TicketHub>("/ticketHub");

app.Run();
