using System.Text.Json.Serialization;

namespace StatisticumDare.Models;

public class EventStats
{
    [JsonPropertyName("status")]
    public int Status { get; set; }

    [JsonPropertyName("caller_id")]
    public int CallerId { get; set; }

    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("subtype")]
    public string? Subtype { get; set; }

    [JsonPropertyName("stats")]
    public Stats? Stats { get; set; }
}

public class Stats
{
    [JsonPropertyName("signups")]
    public int Signups { get; set; }

    [JsonPropertyName("authors")]
    public int Authors { get; set; }

    [JsonPropertyName("unpublished")]
    public int Unpublished { get; set; }

    [JsonPropertyName("game")]
    public int Game { get; set; }

    [JsonPropertyName("craft")]
    public int Craft { get; set; }

    [JsonPropertyName("tool")]
    public int Tool { get; set; }

    [JsonPropertyName("demo")]
    public int Demo { get; set; }

    [JsonPropertyName("jam")]
    public int Jam { get; set; }

    [JsonPropertyName("compo")]
    public int Compo { get; set; }

    [JsonPropertyName("warmup")]
    public int Warmup { get; set; }

    [JsonPropertyName("extra")]
    public int Extra { get; set; }

    [JsonPropertyName("release")]
    public int Release { get; set; }

    [JsonPropertyName("unfinished")]
    public int Unfinished { get; set; }

    [JsonPropertyName("grade-20-plus")]
    public int Grade20Plus { get; set; }

    [JsonPropertyName("grade-15-20")]
    public int Grade1520 { get; set; }

    [JsonPropertyName("grade-10-15")]
    public int Grade1015 { get; set; }

    [JsonPropertyName("grade-5-10")]
    public int Grade510 { get; set; }

    [JsonPropertyName("grade-0-5")]
    public int Grade05 { get; set; }

    [JsonPropertyName("grade-0-only")]
    public int Grade0Only { get; set; }

    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; }

    public int GetCompetitors(string format)
    {
        switch (format)
        {
            case "compo": return Compo;
            case "jam": return Jam;
            case "extra": return Extra;
            case "unfinished": return Unfinished;
            default: return Signups;
        }
    }
}
