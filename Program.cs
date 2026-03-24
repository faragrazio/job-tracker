using JobTracker.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Legge la stringa di connessione da appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Registra il DbContext con MySQL — principio D di SOLID:
// il Controller non crea il DbContext, lo riceve dall'esterno
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

// Aggiunge il supporto per Controller e Views (pattern MVC)
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Route di default: Controller=Home, Action=Index
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Candidature}/{action=Index}/{id?}");

app.Run();