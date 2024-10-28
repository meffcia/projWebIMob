var builder = WebApplication.CreateBuilder(args);

// Dodaj us³ugi do kontenera
builder.Services.AddControllersWithViews();

// Umo¿liwienie korzystania z sesji
builder.Services.AddDistributedMemoryCache(); // Wymagane do u¿ycia sesji
builder.Services.AddSession(options =>
{
    // Ustawienia sesji
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Czas trwania sesji
    options.Cookie.HttpOnly = true; // Zapobiega dostêpowi do ciasteczek sesji przez JavaScript
    options.Cookie.IsEssential = true; // Ciasteczko niezbêdne do prawid³owego dzia³ania sesji
});

var app = builder.Build();

// Konfiguracja potoku ¿¹dañ HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); // U¿yj strony b³êdu w przypadku wyj¹tków
    app.UseHsts(); // W³¹czenie HSTS dla zwiêkszenia bezpieczeñstwa
}

app.UseHttpsRedirection(); // Przekierowanie HTTP na HTTPS
app.UseStaticFiles(); // Umo¿liwienie serwowania plików statycznych

app.UseSession(); // W³¹czenie middleware sesji

app.UseRouting(); // W³¹czenie routingu

app.UseAuthorization(); // Umo¿liwienie autoryzacji

// Mapowanie tras
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Uruchomienie aplikacji
app.Run();
