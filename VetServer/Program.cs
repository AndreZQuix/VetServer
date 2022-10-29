using VetServer.Data;
using Microsoft.EntityFrameworkCore;
using System;
using VetServer.Models.Interfaces;
using VetServer.Models.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//var serverVersion = new MySqlServerVersion(new Version(8, 0, 31));
//builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), serverVersion));

string connectionStr = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContextPool<ApplicationDbContext>(options =>
{
    options.UseMySql(
        connectionStr,
        ServerVersion.AutoDetect(connectionStr),
        options => options.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: System.TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null)
        );
});

builder.Services.AddScoped<IPetRepository, PetRepository>();
builder.Services.AddScoped<IPetParametersRepository, PetParametersRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();