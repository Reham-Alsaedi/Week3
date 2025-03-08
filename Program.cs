using Microsoft.EntityFrameworkCore;
using DocumentVerificationAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Get the connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add DbContext with SQLite provider
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connectionString));  // Use SQLite instead of SQL Server

// Add controllers to the DI container
builder.Services.AddControllers();

// Optional: Add other services as needed
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200/") // Adjust port if needed
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


var app = builder.Build();

// Apply migrations and seed sample data
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate(); // Ensure migrations are applied
    AppDbContext.SeedData(dbContext); // Seed the sample data
}

// Configure the HTTP request pipeline
app.UseHttpsRedirection();

// Map controllers (API routes)
app.MapControllers();

app.Run();


