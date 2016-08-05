using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PlacesApi;
using PlacesApi.JsonBindings;
using System.Globalization;

namespace PlacesApi
{

  public class GooglePlacesApi : IPlacesApi
  {
    private const String _apiKey = "AIzaSyCkc8aQYKa224YJ2QzjHDn-weGL1r83iIA";
    private const String _apiUrl = "https://maps.googleapis.com/maps/api/place";
    private const Int32 _defaultSearchRadius = 200;
    private String[] _defaultSearchTypes = new String[] { "food", "bar", "restaurant" };


    private async Task<HttpResponseMessage> GetDataFromGoogle(String url)
    {
      using (var client = new HttpClient())
      {
        return await client.GetAsync(string.Format($"{_apiUrl}{url}&key={_apiKey}"));
      }
    }

    public async Task<IEnumerable<Place>> GetMapData(Coordinates position)
    {
      List<Place> places = new List<Place>();
      PlacesApiQueryResponse apiResponse = null;
      do
      {
        var coords = position.Latitude.ToString(CultureInfo.CreateSpecificCulture("en-US")) + "," + position.Longitude.ToString(CultureInfo.CreateSpecificCulture("en-US"));
        var types = String.Join(",", _defaultSearchTypes);
        var query = $"/nearbysearch/json?location={coords}&radius={_defaultSearchRadius}&types={types}";
        if (apiResponse != null && !String.IsNullOrEmpty(apiResponse.next_page_token))
          query += "&pagetoken=" + apiResponse.next_page_token;
        var response = await GetDataFromGoogle(query);
        string responseString = await response.Content.ReadAsStringAsync();
        apiResponse = JsonConvert.DeserializeObject<PlacesApiQueryResponse>(responseString);

        foreach (var googlePlace in apiResponse.results)
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
          place.IsOpenNow = googlePlace.opening_hours?.open_now ?? false;

          places.Add(place);
        }

        if (apiResponse.next_page_token != null)
          System.Threading.Thread.Sleep(2000); // wait for page token to be enabled at Google
      }
      while (apiResponse != null && apiResponse.next_page_token != null);

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
        PlaceId = result.result.place_id,
        Rating = result.result.rating,
        PhoneNumber = result.result.formatted_phone_number,
        Address = result.result.formatted_address
      };

    }
  }
}
