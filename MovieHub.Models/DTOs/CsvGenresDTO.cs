namespace MovieHub.Models.DTOs
{
    using Newtonsoft.Json;
    using System;

    public class CsvGenresDTO
    {
        [JsonProperty(PropertyName = "Genre")]
        public string GenresCSV { get; set; }
    }
}
