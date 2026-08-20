using SysNova.Web.Components;
using SysNova.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// ==========================================
// RAZOR COMPONENTS
// ==========================================

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


// ==========================================
// HTTP CLIENT
// ==========================================

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:44341/")
});


// ==========================================
// AUTENTICACIÓN / ESTADO DEL USUARIO
// ==========================================

builder.Services.AddScoped<AuthStateService>();


// ==========================================
// CARRITO DE COMPRAS
// ==========================================

builder.Services.AddScoped<CarritoService>();


var app = builder.Build();


// ==========================================
// CONFIGURACIÓN HTTP
// ==========================================

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(
        "/Error",
        createScopeForErrors: true);

    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAntiforgery();


// ==========================================
// RAZOR COMPONENTS
// ==========================================

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();


app.Run();