using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlacesApi;

namespace GooglePlacesCLI
{
  class Program
  {
    static void Main(string[] args)
    {
      Task.Run(async () =>
      {
        IPlacesApi gapi = new GooglePlacesApi();

        Coordinates ici = new Coordinates()
        {
          Latitude = 48.869194,
          Longitude = 2.343365
        };
        
        var restoPasLoins = await gapi.GetMapData(ici);

        Console.WriteLine(restoPasLoins.Count());


        foreach (var resto in restoPasLoins)
        {
          var details = await gapi.GetPlaceDetails(resto.Id);
          Console.WriteLine("{0} - {1}, Ouvert: {2}", resto.Name, details.Rating, resto.IsOpenNow);
        }

        Console.Read();
      }).Wait();
 
    }
    
  }
}
