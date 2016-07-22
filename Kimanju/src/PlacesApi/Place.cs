using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlacesApi
{
  public class Place
  {
    public String Id { get; set; }
    public Coordinates Location { get; set; }
    public String Name { get; set; }
    public Boolean IsOpenNow { get; set; }
    public PlaceType Type { get; set; }

    public static PlaceType GetPlaceTypes(List<String> types)
    {
      PlaceType type = 0;
      if (types.Contains("restaurant"))
        type |= PlaceType.Restaurant;
      if (types.Contains("bar"))
        type |= PlaceType.Bar;
      if (types.Contains("Food"))
        type |= PlaceType.Food;

      return type;
    }
  }

  [Flags]
  public enum PlaceType
  {
    Restaurant = 1,
    Food = 2,
    Bar = 4
  }
}
