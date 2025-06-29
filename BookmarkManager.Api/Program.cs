using BookmarkManager.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Add custom services
builder.Services.ConfigureServices(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontendLocalhost",
        policy => policy
            .WithOrigins(
                "http://localhost:4200", // Angular
                "http://localhost:3000"  // React
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Swagger removed due to missing package
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowFrontendLocalhost");

app.MapControllers();

app.Run(); 