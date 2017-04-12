using Newtonsoft.Json;

namespace MovieHub.Models.DTOs
{
    public class CsvDirectorsDTO
    {
        [JsonProperty(PropertyName = "Director")]
        public string Directors { get; set; }
    }
}
