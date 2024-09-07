using EstudoMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EstudoMVC.DataContent
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<MVC_DbContext>();

                if (context == null)
                {
                    throw new InvalidOperationException("Não foi possível obter o contexto do banco de dados.");
                }

                context.Database.EnsureCreated();

                if (!context.TouristAttractions.Any())
                {
                    context.TouristAttractions.AddRange(new List<TouristAttraction>()
                    {
                        new TouristAttraction()
                        {
                            Name = "Monte Fuji",
                            Image = "https://pt.wikipedia.org/wiki/Monte_Fuji#/media/Ficheiro:Mount_Fuji_from_Lake_Shoji_(15443819010).jpg",
                            Description = "O monte Fuji (em japonês 富士山 Fuji-san) é a mais alta montanha da ilha de Honshu e de todo o arquipélago japonês. É um vulcão ativo, porém de baixo risco de erupção.",
                            ReviewsAvg = 0,
                        },
                        new TouristAttraction()
                        {
                            Name = "Cristo Redentor",
                            Image = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/4f/Christ_the_Redeemer_-_Cristo_Redentor.jpg/800px-Christ_the_Redeemer_-_Cristo_Redentor.jpg",
                            Description = "Cristo Redentor é uma estátua que retrata Jesus Cristo localizada no topo do morro do Corcovado, a 709 metros acima do nível do mar, com vista para parte considerável da cidade brasileira do Rio de Janeiro.",
                            ReviewsAvg = 0,
                        },
                    });
                    context.SaveChanges();
                }

                //var alterarImagem = context.TouristAttractions.FirstOrDefault(t => t.Name == "Monte Fuji");
                //if (alterarImagem != null)
                //{
                //    alterarImagem.Image = "https://upload.wikimedia.org/wikipedia/commons/6/60/Mount_Fuji_from_Lake_Shoji_%2815443819010%29.jpg";
                //    context.TouristAttractions.Update(alterarImagem);
                //}
                //context.SaveChanges();
          
                if (!context.Reviews.Any())
                {
                    context.Reviews.AddRange(new List<Review>()
                    {
                        new Review
                        {
                            Content = "Incrível experiência",
                            CreationDate = DateTime.UtcNow,
                            MainExperience = Enum.ExperienceType.Artistico,
                            SideExperience = null,
                            Score = Enum.Score.Satisfeito,
                            TouristAttractionId = context.TouristAttractions.First().Id,
                            //UserId = context.Users.First().Id,
                        }
                    });
                    context.SaveChanges();
                }
            }
        }

        public static async Task SeedUrserAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if(!await roleManager.RoleExistsAsync(UserRoles.Admin))
                {
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                }
                if(!await roleManager.RoleExistsAsync(UserRoles.User))
                {
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                }

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
                string adminUserEmail = "jonathaniha.dev@gmail.com";

                var adimUser = await userManager.FindByEmailAsync(adminUserEmail);

                if (adimUser == null) 
                {
                    var newAdminUser = new User()
                    {
                        UserName = "JonathanIha",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Birth = new DateTime(2000, 2, 13),
                        LockoutEnabled = false,
                        LockoutEnd = null
                    };
                    var result = await userManager.CreateAsync(newAdminUser, "Password123@");
                    if(result.Succeeded)
                        await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@etickets.com";
                var appUser = await userManager.FindByEmailAsync(appUserEmail);

                if (appUser == null) 
                {
                    var newAppUser = new User()
                    {
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        Birth = new DateTime(2004, 1, 10),
                        LockoutEnabled = false,
                        LockoutEnd = null
                    };
                    var result = await userManager.CreateAsync(newAppUser, "Password123@");
                    if (result.Succeeded)
                        await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
