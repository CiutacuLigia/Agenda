using Agenda.Components;

var builder = WebApplication.CreateBuilder(args);




// Adaugă pagina pentru excepții de dezvoltare
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Adaugă serviciile pentru Razor Components și Interactive Server
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configurează pipeline-ul de cereri HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // HSTS pentru producție
    app.UseHsts();
    app.UseMigrationsEndPoint(); // Poți elimina această linie, dacă nu folosești EF
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

// Configurează componentele Razor
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
