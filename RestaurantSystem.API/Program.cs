using RestaurantSystem.Infrastructure;
using Microsoft.OpenApi.Models;
using AutoMapper;
using RestaurantSystem.Application.MappingProfiles;
using RestaurantSystem.Domain.Interfaces;
using RestaurantSystem.Infrastructure.Repositories;
using RestaurantSystem.Application.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MenuItemProfile));



// Register Infrastructure Layer (Database + Repositories + etc.)
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();
builder.Services.AddScoped<MenuItemService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
