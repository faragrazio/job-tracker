using JobTracker.Models;
using JobTracker.Repositories;
using JobTracker.UseCases;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Registra il DbContext con MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

// Registra il Repository — layer di accesso ai dati
builder.Services.AddScoped<ICandidaturaRepository, CandidaturaRepository>();

// Registra gli UseCases — layer di logica business
builder.Services.AddScoped<GetAllCandidatureUseCase>();
builder.Services.AddScoped<GetCandidaturaByIdUseCase>();
builder.Services.AddScoped<CreateCandidaturaUseCase>();
builder.Services.AddScoped<UpdateCandidaturaUseCase>();
builder.Services.AddScoped<DeleteCandidaturaUseCase>();
builder.Services.AddScoped<GetStatsUseCase>();
builder.Services.AddScoped<GetTimelineUseCase>();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Candidature}/{action=Index}/{id?}");

app.Run();