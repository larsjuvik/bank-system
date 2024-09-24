using AutoMapper;
using Data;
using Data.Models;
using Data.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using WebApp.Components;
using WebApp.DTOs;
using WebApp.Services;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Repositories
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<TransactionRepository>();
builder.Services.AddScoped<BankAccountRepository>();

// Services
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<TransactionService>();
builder.Services.AddScoped<BankAccountService>();

// AutoMapper
var autoMapperConfig = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<RegisterDto, User>();

    // Transaction
    cfg.CreateMap<Transaction, TransactionDto>();

    // Bank account
    cfg.CreateMap<BankAccount, BankAccountDto>();

    // User
    cfg.CreateMap<User, UserDto>();
    cfg.CreateMap<EditUserDto, User>();
    cfg.CreateMap<User, EditUserDto>();
});
builder.Services.AddSingleton(autoMapperConfig.CreateMapper());

// Database
builder.Services.AddDbContext<BankContext>(options => options.UseInMemoryDatabase("BankSystemDemo"));

// Authentication and authorization
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "Authentication";
        options.LoginPath = "/login";
        options.LogoutPath = "/logout";
        options.AccessDeniedPath = "/access-denied";
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
    });
builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddMudServices();

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

app.UseStatusCodePages();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict
});

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseAntiforgery();

app.UseEndpoints(options =>
{
    options.MapControllers();
});

app.Run();
