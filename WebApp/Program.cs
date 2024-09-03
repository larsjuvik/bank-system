using AutoMapper;
using BankSystem.Data;
using Data.Models;
using Data.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using WebApp.Components;
using WebApp.DTOs;
using WebApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Repostiories
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<TransactionRepository>();

// Services
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<TransactionService>();

// AutoMapper
var autoMapperConfig = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<RegisterDTO, User>();

    // Transaction
    cfg.CreateMap<Transaction, TransactionDTO>();

    // Bank account
    cfg.CreateMap<BankAccount, BankAccountDTO>();

    // User
    cfg.CreateMap<User, UserDTO>();
});
builder.Services.AddSingleton(autoMapperConfig.CreateMapper());

builder.Services.AddDbContext<BankContext>(options => options.UseInMemoryDatabase("BankSystemDemo"));

// Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "BankSystemDemo";
        options.LoginPath = "/login";
        options.LogoutPath = "/logout";
        options.AccessDeniedPath = "/access-denied";
        options.Cookie.MaxAge = TimeSpan.FromDays(30);
    });
builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();
var app = builder.Build();

// Ensure the database is created.
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<BankContext>();
    dbContext.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
