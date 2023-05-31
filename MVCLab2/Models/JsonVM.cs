

using System.Text.Json.Serialization;

namespace MVCLab2.Models
{
  public class JsonVM
  {
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

  }
}
