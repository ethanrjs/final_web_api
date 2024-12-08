// Ethan Rettinger
using Final_Project_Web_API.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddOpenApiDocument(configure =>
{
    configure.Title = "Ethan's WebAPI";
});

var app = builder.Build();

// HTTP Request Pipeline
app.UseHttpsRedirection();

// NSwag Middleware
app.UseOpenApi();
app.UseSwaggerUi();
app.UseAuthorization();
app.MapControllers();

app.Run();