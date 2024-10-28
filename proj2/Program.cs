var builder = WebApplication.CreateBuilder(args);

// Dodaj us�ugi do kontenera
builder.Services.AddControllersWithViews();

// Umo�liwienie korzystania z sesji
builder.Services.AddDistributedMemoryCache(); // Wymagane do u�ycia sesji
builder.Services.AddSession(options =>
{
    // Ustawienia sesji
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Czas trwania sesji
    options.Cookie.HttpOnly = true; // Zapobiega dost�powi do ciasteczek sesji przez JavaScript
    options.Cookie.IsEssential = true; // Ciasteczko niezb�dne do prawid�owego dzia�ania sesji
});

var app = builder.Build();

// Konfiguracja potoku ��da� HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // U�yj strony b��du w przypadku wyj�tk�w
    app.UseHsts(); // W��czenie HSTS dla zwi�kszenia bezpiecze�stwa
}

app.UseHttpsRedirection(); // Przekierowanie HTTP na HTTPS
app.UseStaticFiles(); // Umo�liwienie serwowania plik�w statycznych

app.UseSession(); // W��czenie middleware sesji

app.UseRouting(); // W��czenie routingu

app.UseAuthorization(); // Umo�liwienie autoryzacji

// Mapowanie tras
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Uruchomienie aplikacji
app.Run();
