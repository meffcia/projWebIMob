var builder = WebApplication.CreateBuilder(args);

// Rejestracja IHttpClientFactory w DI
builder.Services.AddHttpClient();

// Rejestracja us³ug MVC
builder.Services.AddControllersWithViews();

// Rejestracja sesji (opcjonalnie, je¿eli jej jeszcze nie skonfigurowano)
builder.Services.AddDistributedMemoryCache(); // Wymagane dla sesji
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Mo¿esz dostosowaæ czas trwania sesji
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Konfiguracja potoku HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

// Umo¿liwia u¿ywanie sesji
app.UseSession();

// Mapowanie domyœlnej trasy
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
