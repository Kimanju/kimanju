using System;
using Newtonsoft.Json;

namespace Kimanju.Models
{
  public interface IKimanjuDocument
  {
    [JsonProperty(PropertyName = "id")]
    Guid Id { get; set; }
  }
}