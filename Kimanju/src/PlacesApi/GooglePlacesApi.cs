using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PlacesApi;
using PlacesApi.JsonBindings;

namespace PlacesApi
{
  
  public class GooglePlacesApi : IPlacesApi
  {
    private const String _apiKey = "AIzaSyCkc8aQYKa224YJ2QzjHDn-weGL1r83iIA";
    private const String _apiUrl = "https://maps.googleapis.com/maps/api/place";
    private const Int32 _defaultSearchRadius = 500;
    private const String _defaultSearchTypes = "food,restaurant,bar";


    private async Task<HttpResponseMessage> GetDataFromGoogle(String url)
    {
      using (var client = new HttpClient())
      {
        return await client.GetAsync(string.Format($"{_apiUrl}{url}&key={_apiKey}"));
      }
    }

    public async Task<IEnumerable<Place>> GetMapData(Coordinates position)
    {
      var response = await GetDataFromGoogle($"/nearbysearch/json?location={position.Latitude}{position.Longitude}&radius={_defaultSearchRadius}&types={_defaultSearchTypes}");
      var result = JsonConvert.DeserializeObject<PlacesApiQueryResponse>(await response.Content.ReadAsStringAsync());

      List<Place> places = new List<Place>();

      foreach (var googlePlace in result.results)
      {
        Place place = new Place();
        place.Location = new Coordinates()
        {
          Latitude = googlePlace.geometry.location.lat,
          Longitude = googlePlace.geometry.location.lng
        };
        place.Id = googlePlace.place_id;
        place.Name = googlePlace.name;
        place.Type = Place.GetPlaceTypes(googlePlace.types);
        place.IsOpenNow = googlePlace.opening_hours.open_now;

        places.Add(place);
      }

      return places;
    }

    public async Task<PlaceDetails> GetPlaceDetails(String placeId)
    {
      var response = await GetDataFromGoogle($"/details/output?placeid={placeId}");
	  var result = JsonConvert.DeserializeObject<PlaceDetailsApiQueryResponse>(await response.Content.ReadAsStringAsync());
	  
	  if (result.result == null)
		  throw new ArgumentOutOfRangeException("Can't find the details of this place", nameof(placeId));
	  
	  return new PlaceDetails()
	  {
		PlaceId = result.place_id,
        Rating = result.rating,
        PhoneNumber = rating.formatted_phone_number,
        Address = rating.formatted_address
	  };

    }
  }
}
