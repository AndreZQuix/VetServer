using VetServer.Data;
using Microsoft.EntityFrameworkCore;
using System;
using VetServer.Models.Interfaces;
using VetServer.Models.Repositories;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

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

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "VetServer API",
        Description = "Process veterinary data with VetServer API",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Andre",
            Email = "andrez.quix@gmail.com",
        }
    });

    //Set the comments path for the swagger json and ui.
    var xmlPath = Path.Combine(AppContext.BaseDirectory, "VetServerApi.xml");
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => { 
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "VetServer API v1");
    c.RoutePrefix = "";
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.Run();
