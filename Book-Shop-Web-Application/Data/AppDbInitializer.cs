using Book_Shop_Web_Application.Data.Static;
using Book_Shop_Web_Application.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;

namespace Book_Shop_Web_Application.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<BookDbContext>();

                context.Database.EnsureCreated();


                //Authors
                if (!context.Authors.Any())
                {
                    context.Authors.Add(new Author()
                    {
                        ProfilePictureURL = "https://upload.wikimedia.org/wikipedia/commons/d/d9/Boles%C5%82aw_Prus_studio_portrait.jpg",
                        FullName = "Bolesław Prus",
                        Bio = "Polski pisarz, prozaik, nowelista i publicysta okresu pozytywizmu, współtwórca polskiego realizmu, kronikarz Warszawy, myśliciel i popularyzator wiedzy, działacz społeczny, propagator turystyki pieszej i rowerowej"
                    });
                    context.SaveChanges();
                }
                //Publishers
                if (!context.Publishers.Any())
                {
                    context.Publishers.Add(new Publisher()
                    {
                        LogoPictureURL = "https://wydawnictwomg.pl/wp-content/uploads/2011/08/logo-MG-2.jpg",
                        Name = "MG",
                        Description = "Oficyna Wydawnicza MG specjalizuje się w książkach polskich autorów"
                    });
                    context.SaveChanges();
                }
                //Books
                if (!context.Books.Any())
                {
                    context.Books.Add(new Book()
                    {
                        Title = "Lalka",
                        Description = "“Lalka” to powieść o świecie, w którym każdy odgrywa jakąś z góry przypisaną rolę i dzielnie trzyma się każdej wypowiadanej kwestii.",
                        Price = 49.90,
                        ImageURL = "https://cf-taniaksiazka.statiki.pl/images/large/111/9788377797235.jpg",
                        YearOfPublication = 2021,
                        AuthorId = 1,
                        PublisherId = 1

                    });
                    context.SaveChanges();
                }
            }
        }
    
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

                var adminUser = await userManager.FindByEmailAsync("admin@bookshop.com");
                if (adminUser == null) 
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        FullName = "Admin User",
                        UserName = "admin",
                        Email = "admin@bookshop.com",
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "Admin@1234!");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                var appUser = await userManager.FindByEmailAsync("user@bookshop.com");
                if (appUser == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        FullName = "App User",
                        UserName = "user01",
                        Email = "user@bookshop.com",
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "User@1234!");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
