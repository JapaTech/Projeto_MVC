using EstudoMVC.Models;

namespace EstudoMVC.DataContent
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var content = serviceScope.ServiceProvider.GetService<MVC_DbContext>();

                content.Database.EnsureCreated();

                if (!content.TouristAttractions.Any())
                {
                    content.TouristAttractions.AddRange(new List<TouristAttraction>()
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
                        }
                    });
                    content.SaveChanges();
                }
            }

        }

    }
}
