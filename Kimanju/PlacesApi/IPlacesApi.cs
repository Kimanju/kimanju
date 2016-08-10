using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kimanju.PlacesApi
{
    public interface IPlacesApi
    {
      Task<IEnumerable<Place>> NearbySearch(Coordinates position, String keyword);
      Task<PlaceDetails> GetPlaceDetails(String placeId);
    }
}
