using System.Text.Json.Serialization;

namespace StatisticumDare.Models;

public class UserProfile
{
    [JsonPropertyName("node_id")]
    public int NodeId { get; set; }
}