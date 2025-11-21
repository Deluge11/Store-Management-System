using Business_Layer.Interfaces;
using Business_Layer.Business;
using Data_Layer.Interfaces;
using Data_Layer.Data;
using Bussiness_Layer.Interfaces;
using Bussiness_Layer.Business;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Options;
using Microsoft.Extensions.DependencyInjection;
using Presentation_Layer.Authentication;
using Presentation_Layer.Authorization;

var builder = WebApplication.CreateBuilder(args);

var connectingString = builder.Configuration.GetConnectionString("Default");
var jwtOptions = builder.Configuration.GetSection("JwtOptions").Get<JwtOptions>();
var ecommerceOptions = builder.Configuration.GetSection("Ecommerce_Inventory_Shared_Key").Get<EcommerceOptions>();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddSingleton<string>(connectingString);
builder.Services.AddSingleton<JwtOptions>(jwtOptions);
builder.Services.AddSingleton<EcommerceOptions>(ecommerceOptions);



builder.Services.AddAuthentication()
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtOptions.Issuer,
            ValidateAudience = true,
            ValidAudience = jwtOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SigningKey))
        };
    })
    .AddJwtBearer("Ecommerce", options =>
    {
        //options.Events = new JwtBearerEvents
        //{
        //    OnAuthenticationFailed = context =>
        //    {
        //        Console.WriteLine("Authentication failed:");
        //        Console.WriteLine(context.Exception.Message);
        //        return Task.CompletedTask;
        //    },
        //    OnTokenValidated = context =>
        //    {
        //        Console.WriteLine("Token validated successfully!");
        //        return Task.CompletedTask;
        //    },
        //    OnMessageReceived = context =>
        //    {
        //        Console.WriteLine($"Token received: {context.Token}");
        //        return Task.CompletedTask;
        //    }
        //};
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = ecommerceOptions.Issuer,
            ValidateAudience = true,
            ValidAudience = ecommerceOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ecommerceOptions.Key))
        };
    });




   

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IOrdersBusiness, OrdersBusiness>();
builder.Services.AddScoped<IStocksBusiness, StocksBusiness>();
builder.Services.AddScoped<ITruckBusiness, TruckBusiness>();
builder.Services.AddScoped<IInventoryBusiness, InventoryBusiness>();
builder.Services.AddScoped<IAccountsBusiness, AccountsBusiness>();
builder.Services.AddScoped<IAuthorizeBusiness, AuthorizeBusiness>();

builder.Services.AddScoped<IOrdersData, OrdersData>();
builder.Services.AddScoped<IStocksData, StocksData>();
builder.Services.AddScoped<ITruckData, TruckData>();
builder.Services.AddScoped<IInventoryData, InventoryData>();
builder.Services.AddScoped<IAccountsData, AccountsData>();
builder.Services.AddScoped<IAuthorizeData, AuthorizeData>();

builder.Services.AddScoped<AuthenticateHelper>();
builder.Services.AddScoped<AuthorizeHelper>();





var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
