using System.Text.Json.Serialization;

namespace StatisticumDare.Models;

public class GameFeed
{
    [JsonPropertyName("feed")]
    public List<Feed>? Feed { get; set; }
}

public class Feed
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("modified")]
    public DateTime Modified { get; set; }
}
