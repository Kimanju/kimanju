using System;
using Newtonsoft.Json;

namespace Kimanju.Models
{
  public class Place : IKimanjuDocument
  {
    public Guid Id { get; set; }
    public String Name { get; set; }
    public String City { get; set; }
    public String Postcode { get; set; }
    public String StreetNumber { get; set; }
    public String StreetName { get; set; }
    public LatLong LatLong { get; set; }
    [JsonProperty(Required = Required.Default)]
    public String Website { get; set; }
    [JsonProperty(Required = Required.Default)]
    public String GooglePlaceId { get; set; }
  }
}