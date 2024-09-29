using AutoMapper;
using Data;
using Data.Models;
using Data.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
using WebApp.Components;
using WebApp.DTOs;
using WebApp.Services;
using MudBlazor.Services;
using WebApp.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Repositories
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<TransactionRepository>();
builder.Services.AddScoped<BankAccountRepository>();
builder.Services.AddScoped<UserLoginRepository>();

// Services
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<TransactionService>();
builder.Services.AddScoped<BankAccountService>();
builder.Services.AddScoped<UserLoginService>();

// App Options
var authenticationSettings = new AuthenticationOptions();
builder.Configuration.GetSection(AuthenticationOptions.SectionKey).Bind(authenticationSettings);
builder.Services.AddSingleton(authenticationSettings);

var cultureOptions = builder.Configuration.GetSection(CultureOptions.SectionKey);
builder.Services.AddSingleton(cultureOptions);

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
    
    // User login
    cfg.CreateMap<UserLogin, UserLoginDto>();
    cfg.CreateMap<UserLoginDto, UserLogin>();
});
builder.Services.AddSingleton(autoMapperConfig.CreateMapper());

// Database
builder.Services.AddDbContext<BankContext>(options => options.UseInMemoryDatabase("BankSystemDemo"));

// Authentication and authorization
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = authenticationSettings.CookieName;
        options.LoginPath = "/login";
        options.LogoutPath = "/logout";
        options.AccessDeniedPath = "/access-denied";
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(authenticationSettings.CookieExpirationMinutes);
        options.SlidingExpiration = true;
    });
builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
});

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
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();
app.MapControllers();

// Close connection if user's authentication cookie has expired
app.MapBlazorHub(options =>
{
    options.CloseOnAuthenticationExpiration = true;
}).WithOrder(-1);

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();
