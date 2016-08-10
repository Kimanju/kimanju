using System;
using System.Linq;
using System.Threading.Tasks;
using Kimanju.PlacesApi;

namespace Kimanju.GooglePlacesCLI
{
  class Program
  {
    static void Main(String[] args)
    {
      Task.Run(async () =>
      {
        Console.Write("Quoi ? : ");
        var keyword = Console.ReadLine();

        IPlacesApi gapi = new GooglePlacesApi();

        var ici = new Coordinates
        {
          Latitude = 48.869194,
          Longitude = 2.343365
        };
        
        var restoPasLoins = await gapi.NearbySearch(ici, keyword);
        var restos = restoPasLoins as Place[] ?? restoPasLoins.ToArray();

        Console.WriteLine($"Pas loin de \"{keyword}\", il y a {restos.Length} lieux :");

        foreach (var resto in restos)
        {
          var details = await gapi.GetPlaceDetails(resto.Id);
          Console.WriteLine($"{resto.Name} - {details.Rating}, Ouvert: {resto.IsOpenNow}");
        }

        Console.Read();
      }).Wait(); 
    }
  }
}