using Book_Shop_Web_Application.Data;
using Book_Shop_Web_Application.Data.Cart;
using Book_Shop_Web_Application.Data.Interfaces;
using Book_Shop_Web_Application.Data.Services;
using Book_Shop_Web_Application.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;


var builder = WebApplication.CreateBuilder(args);

//DbContext configuration
builder.Services.AddDbContext<BookDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//Services configuration
builder.Services.AddScoped<IPublishersService, PublishersService>();
builder.Services.AddScoped<IAuthorsService, AuthorsService>();
builder.Services.AddScoped<IBooksService, BooksService>();
builder.Services.AddScoped<IOrdersService, OrdersService>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped(sc => ShoppingCart.GetShoppingCart(sc));

//Authentication and authorization
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 4;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
}).AddEntityFrameworkStores<BookDbContext>();

builder.Services.AddMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
});

builder.Services.AddRazorPages();

builder.Services.AddControllersWithViews();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

//Authentication & Authorization
app.UseAuthentication();
app.UseAuthorization();

app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "books",
        pattern: "/ksiazki",
        defaults: new { controller = "Books", action = "Index" }
        );
    endpoints.MapControllerRoute(
        name: "books",
        pattern: "/ksiazki/{id}/szczegoly",
        defaults: new { controller = "Books", action = "Details" }
        );
    endpoints.MapControllerRoute(
        name: "books",
        pattern: "/ksiazki/{id}/edytuj",
        defaults: new { controller = "Books", action = "Edit" }
        );
    endpoints.MapControllerRoute(
        name: "books",
        pattern: "/ksiazki/dodaj",
        defaults: new { controller = "Books", action = "Create" }
        );
    endpoints.MapControllerRoute(
        name: "orders",
        pattern: "/ksiazki/zamowienia",
        defaults: new { controller = "Orders", action = "Index" }
        );
    endpoints.MapControllerRoute(
        name: "orders",
        pattern: "/ksiazki/koszyk/dodaj",
        defaults: new { controller = "Orders", action = "AddToShoppingCart" }
        );
    endpoints.MapControllerRoute(
        name: "orders",
        pattern: "/ksiazki/koszyk/usun",
        defaults: new { controller = "Orders", action = "RemoveFromShoppingCart" }
        );
    endpoints.MapControllerRoute(
        name: "orders",
        pattern: "/ksiazki/koszyk/sfinalizuj",
        defaults: new { controller = "Orders", action = "CompleteOrder" }
        );
    endpoints.MapControllerRoute(
        name: "orders",
        pattern: "/ksiazki/koszyk/podsumowanie",
        defaults: new { controller = "Orders", action = "ShoppingCart" }
        );
    endpoints.MapControllerRoute(
        name: "authors",
        pattern: "/autorzy",
        defaults: new { controller = "Authors", action = "Index" }
        );
    endpoints.MapControllerRoute(
        name: "authors",
        pattern: "/autorzy/szczegoly",
        defaults: new { controller = "Authors", action = "Details" }
        );
    endpoints.MapControllerRoute(
        name: "publishers",
        pattern: "/wydawnictwa",
        defaults: new { controller = "Publishers", action = "Index" }
        );
    endpoints.MapControllerRoute(
        name: "publishers",
        pattern: "/wydawnictwa/szczegoly",
        defaults: new { controller = "Publishers", action = "Details" }
        );
    endpoints.MapControllerRoute(
        name: "account",
        pattern: "/uzytkownicy",
        defaults: new { controller = "Account", action = "Users" }
        );
    endpoints.MapControllerRoute(
        name: "account",
        pattern: "/logowanie",
        defaults: new { controller = "Account", action = "Login" }
        );
    endpoints.MapControllerRoute(
        name: "account",
        pattern: "/rejestracja",
        defaults: new { controller = "Account", action = "Register" }
        );
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Books}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
});


//Seed database
AppDbInitializer.Seed(app);
AppDbInitializer.SeedUsersAndRolesAsync(app).Wait();

app.Run();