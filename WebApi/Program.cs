using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using PersonsBLL.Common;
using PersonsBLL.Interfaces;
using PersonsBLL.Services;
using PersonsDAL.Data;
using PersonsDAL.Interfaces;
using PersonsDAL.Repository;
using System;
using System.Globalization;
using WebApi.Filters;
using WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

// ==============================
// 2?? Dependency Injection
// ==============================
builder.Services.AddDbContext<AppDbContext>(opts =>
{
    opts.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        options => options.EnableRetryOnFailure());
});

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");


builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();

// ==============================
// 3?? Configure Controllers & Filters
// ==============================
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ValidateModelAttribute());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseRequestLocalization(options =>
{
    options.SetDefaultCulture("ka-GE")
           .AddSupportedCultures("en-US", "ka-GE") // Add other supported cultures
           .AddSupportedUICultures("en-US", "ka-GE"); // Specify supported UICultures
});

app.UseMiddleware<ExceptionMiddleware>();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    SeedData.SeedAppData(dbContext);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthorization();

// ? Register Controllers
app.MapControllers();

// ? Run the Application
app.Run();