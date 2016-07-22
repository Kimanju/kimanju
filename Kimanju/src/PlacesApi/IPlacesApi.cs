using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlacesApi
{
    public interface IPlacesApi
    {
      Task<IEnumerable<Place>> GetMapData(Coordinates position);
      Task<PlaceDetails> GetPlaceDetails(String placeId);
    }
}
