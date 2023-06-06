using System.Text.Json.Serialization;

namespace StatisticumDare.Models;

public class LudumDareGameData
{
    [JsonPropertyName("games")]
    public List<Game>? Games { get; set; }
}

public class Game
{
    [JsonPropertyName("cover")]
    public string? Cover { get; set; }

    [JsonPropertyName("path")]
    public string? Path { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("format")]
    public string? Format { get; set; }

    [JsonPropertyName("submittedDate")]
    public DateTime? SubmittedDate { get; set; }

    [JsonPropertyName("overall")]
    public Category? Overall { get; set; }

    [JsonPropertyName("fun")]
    public Category? Fun { get; set; }

    [JsonPropertyName("innovation")]
    public Category? Innovation { get; set; }

    [JsonPropertyName("theme")]
    public Category? Theme { get; set; }

    [JsonPropertyName("graphics")]
    public Category? Graphics { get; set; }

    [JsonPropertyName("audio")]
    public Category? Audio { get; set; }

    [JsonPropertyName("humor")]
    public Category? Humor { get; set; }

    [JsonPropertyName("mood")]
    public Category? Mood { get; set; }

    [JsonPropertyName("edition")]
    public int Edition { get; set; }

    [JsonPropertyName("categoryCompetitors")]
    public int CategoryCompetitors { get; set; }
}

public class Category
{
    [JsonPropertyName("totalScore")]
    public int? TotalScore { get; set; }

    [JsonPropertyName("place")]
    public int? Result { get; set; }

    [JsonPropertyName("averageScore")]
    public double? AverageScore { get; set; }

    [JsonPropertyName("Percentile")]
    public double? Percentile { get; set; }

    public static double CalculatePercentile(int categoryCompetitors, int? result)
    {
        if (result == 0 || result == null)
            return 0;
        else
            return (double)(categoryCompetitors - result) / categoryCompetitors * 100;
    }
}
