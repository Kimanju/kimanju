using System;
using Newtonsoft.Json;

namespace Kimanju.Models
{
  public class User : IKimanjuDocument
  {
    public Guid Id { get; set; }
    public String Name { get; set; }
    [JsonProperty(Required = Required.Default)]
    public Guid[] Wishes { get; set; }
  }
}