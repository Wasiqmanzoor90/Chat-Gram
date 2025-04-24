using FluentValidation;
using FluentValidation.AspNetCore;
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
//Call JWT here
builder.Services.AddJwtAuthentication(builder.Configuration);

// Register MediatR (v12+ syntax)
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});


builder.Services.AddCors(Options =>
{
    Options.AddPolicy("AllowAll", policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});


var app = builder.Build();





// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
