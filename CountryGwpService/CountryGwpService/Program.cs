using CountryGwpService.DataLayer;
using CountryGwpService.DataLayer.Services;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<IDataService, DataService>();
builder.Services.AddSingleton<IGwpRepository, GwpRepository>(); // Register repository

// Add services to the container.
builder.Services.AddControllers();
// For API documentation
// For Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();




// Initialize the DataService
using (var scope = app.Services.CreateScope())
{
    var dataService = scope.ServiceProvider.GetRequiredService<IDataService>();
    dataService.Initialize();
}


// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    // Enable Swagger UI
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty; // Swagger UI at the root
    });
}

// Enable endpoint routing
app.UseRouting();


// Map controllers
app.MapControllers();

app.Run();
