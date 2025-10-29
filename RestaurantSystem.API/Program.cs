using RestaurantSystem.Infrastructure;
using Microsoft.OpenApi.Models;
using AutoMapper;
using RestaurantSystem.Application.MappingProfiles;
using RestaurantSystem.Domain.Interfaces;
using RestaurantSystem.Infrastructure.Repositories;
using RestaurantSystem.Application.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGenNewtonsoftSupport();
builder.Services.AddAutoMapper(typeof(CategoryProfile));
builder.Services.AddAutoMapper(typeof(MenuItemProfile).Assembly);
//builder.Services.AddControllers().AddNewtonsoftJson();


// Register Infrastructure Layer (Database + Repositories + etc.)
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();
builder.Services.AddScoped<MenuItemService>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<CategoryService>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddScoped<OrderService>();

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
