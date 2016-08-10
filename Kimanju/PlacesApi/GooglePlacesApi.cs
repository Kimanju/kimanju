using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Kimanju.PlacesApi.JsonBindings;

namespace Kimanju.PlacesApi
{

  public class GooglePlacesApi : IPlacesApi
  {
    private const String _apiKey = "AIzaSyCkc8aQYKa224YJ2QzjHDn-weGL1r83iIA";
    private const String _apiUrl = "https://maps.googleapis.com/maps/api/place";
    private const Int32 _defaultSearchRadiusMeters = 200;
    private String[] _defaultSearchTypes = { "food", "bar", "restaurant" };


    private async Task<HttpResponseMessage> GetDataFromGoogle(String url)
    {
      using (var client = new HttpClient())
      {
        return await client.GetAsync(String.Format($"{_apiUrl}{url}&key={_apiKey}"));
      }
    }

    public async Task<IEnumerable<Place>> NearbySearch(Coordinates position, String keyword)
    {
      List<Place> places = new List<Place>();
      PlacesApiQueryResponse apiResponse = null;
      do
      {
        var coords = position.Latitude.ToString(CultureInfo.CreateSpecificCulture("en-US")) + "," + position.Longitude.ToString(CultureInfo.CreateSpecificCulture("en-US"));
        var types = String.Join("|", _defaultSearchTypes);
        var query = $"/nearbysearch/json?location={coords}&radius={_defaultSearchRadiusMeters}&types={types}";
        if (!String.IsNullOrEmpty(keyword))
          query += "&keyword=" + Uri.EscapeDataString(keyword);
        if (!String.IsNullOrEmpty(apiResponse?.next_page_token))
          query += "&pagetoken=" + apiResponse.next_page_token;
        var response = await GetDataFromGoogle(query);
        var responseString = await response.Content.ReadAsStringAsync();
        apiResponse = JsonConvert.DeserializeObject<PlacesApiQueryResponse>(responseString);

        places.AddRange(apiResponse.results.Select(googlePlace => new Place
        {
          Location = new Coordinates
          {
            Latitude = googlePlace.geometry.location.lat,
            Longitude = googlePlace.geometry.location.lng
          },
          Id = googlePlace.place_id,
          Name = googlePlace.name,
          Type = Place.GetPlaceTypes(googlePlace.types),
          IsOpenNow = googlePlace.opening_hours?.open_now ?? false
        }));

        if (apiResponse.next_page_token != null)
          System.Threading.Thread.Sleep(2000); // wait for page token to be enabled at Google
      }
      while (apiResponse.next_page_token != null);

      return places;
    }

    public async Task<PlaceDetails> GetPlaceDetails(String placeId)
    {
      var response = await GetDataFromGoogle($"/details/json?placeid={placeId}");
      var result = JsonConvert.DeserializeObject<PlaceDetailsApiQueryResponse>(await response.Content.ReadAsStringAsync());

      if (result.result == null)
        throw new ArgumentOutOfRangeException(nameof(placeId), placeId, "Can't find the details of this place.");

      return new PlaceDetails
      {
        PlaceId = result.result.place_id,
        Rating = result.result.rating,
        PhoneNumber = result.result.formatted_phone_number,
        Address = result.result.formatted_address
      };
    }
  }
}