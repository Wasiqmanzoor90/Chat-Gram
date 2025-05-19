using FluentValidation.AspNetCore;
using FluentValidation;
using Server.Application.Interface;
using Server.Application.Service;
using Server.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation(); // Auto model validation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddSingleton<MongoDbService>();
builder.Services.AddScoped<ICloudinaryInterface, CloudinaryService>();
builder.Services.AddScoped<IJToken, TokenService>();

// Call JWT authentication
builder.Services.AddJwtAuthentication(builder.Configuration);

// Register MediatR (v12+ syntax)
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});

// Add CORS policy for React frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // ✅ React app URL
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // ✅ Important!
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Add CORS before authentication and authorization
app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
