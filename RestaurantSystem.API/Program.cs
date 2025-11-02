using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RestaurantSystem.API.Middlewares;
using RestaurantSystem.Application.Interfaces;
using RestaurantSystem.Application.MappingProfiles;
using RestaurantSystem.Application.Services;
using RestaurantSystem.Domain.Entities;
using RestaurantSystem.Domain.Interfaces;
using RestaurantSystem.Infrastructure;
using RestaurantSystem.Infrastructure.Persistence;
using RestaurantSystem.Infrastructure.Repositories;
using RestaurantSystem.Infrastructure.Seeders;
using System.Text;


var builder = WebApplication.CreateBuilder(args);



// Add services to the container
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenNewtonsoftSupport();
builder.Services.AddAutoMapper(typeof(CategoryProfile));
builder.Services.AddAutoMapper(typeof(MenuItemProfile).Assembly);
builder.Services.AddAutoMapper(typeof(OrderProfile).Assembly);
builder.Services.AddAutoMapper(typeof(CustomerProfile));


builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Restaurant System API",
        Version = "v1",
        Description = "API documentation for the Restaurant System"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT token below (Example: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6...)"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});


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

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<CustomerService>();

builder.Services.AddScoped<IAuthRepository , AuthRepository>();
builder.Services.AddScoped<IAuthService , AuthService>();

builder.Services.AddTransient<DatabaseSeeder>();


builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

var jwtkey = builder.Configuration["Jwt:Key"];
var jwtsetting = builder.Configuration.GetSection("Jwt");
builder.Services.AddAuthentication(option =>

{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

})
    .AddJwtBearer(option =>
    {
        option.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtsetting["Issuer"],
            ValidAudience = jwtsetting["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtkey))

        };
    });

builder.Services.AddAuthorization();


var app = builder.Build();


using (var scope = app.Services.CreateScope()) {

    var sedder = scope.ServiceProvider.GetRequiredService<DatabaseSeeder>();
    await sedder.SeedAsync();
}

    // Configure the HTTP request pipeline
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }


app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
