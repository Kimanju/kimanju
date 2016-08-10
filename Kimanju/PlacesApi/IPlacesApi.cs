using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kimanju.PlacesApi
{
    public interface IPlacesApi
    {
      Task<IEnumerable<Place>> GetMapData(Coordinates position);
      Task<PlaceDetails> GetPlaceDetails(String placeId);
    }
}
