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
// 1?? Configure Localization Services
// ==============================
var supportedCultures = new[]
{
    new CultureInfo("en-US"),
    new CultureInfo("en"),
    new CultureInfo("ka-GE"),
    new CultureInfo("ka")
};

var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en-US"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
};

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

// ==============================
// 2?? Dependency Injection
// ==============================
builder.Services.AddDbContext<AppDbContext>(opts =>
{
    opts.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        options => options.EnableRetryOnFailure());
});

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

// ==============================
// 4?? Apply Localization Before Controllers
// ==============================
app.UseRequestLocalization(localizationOptions);

// ==============================
// 5?? Apply Exception Handling Middleware
// ==============================
app.UseMiddleware<ExceptionMiddleware>();

// ==============================
// 6?? Ensure Database is Populated
// ==============================
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    SeedData.SeedAppData(dbContext);
}

// ==============================
// 7?? Configure Request Pipeline
// ==============================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ? Apply Authentication & Authorization Middleware
app.UseAuthorization();

// ? Register Controllers
app.MapControllers();

// ? Run the Application
app.Run();