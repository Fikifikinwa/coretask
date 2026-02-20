using CoreTask.Api.Data;
using CoreTask.Api.Middleware;
using CoreTask.Api.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/coretasks-.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 7)
    .CreateLogger();

builder.Host.UseSerilog();

// Add services
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddDbContext<CoreTasksDbContext>(options => options.UseSqlite("Data Source=coretasks.db"));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddEndpointsApiExplorer();

//  Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()
    );
});

builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
});


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ezra To-Do API v1");
        c.RoutePrefix = string.Empty; // Swagger UI at root
    });
}

// Custom middleware
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();

app.Run();