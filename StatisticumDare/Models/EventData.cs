using System.Text.Json.Serialization;

namespace StatisticumDare.Models;

public class EventData
{
    [JsonPropertyName("status")]
    public int Status { get; set; }

    [JsonPropertyName("caller_id")]
    public int CallerId { get; set; }

    [JsonPropertyName("root")]
    public int Root { get; set; }

    [JsonPropertyName("path")]
    public List<int>? Path { get; set; }

    [JsonPropertyName("extra")]
    public List<string>? Extra { get; set; }

    [JsonPropertyName("node_id")]
    public int NodeId { get; set; }

    [JsonPropertyName("nodes_cached")]
    public List<int>? NodesCached { get; set; }

    [JsonPropertyName("node")]
    public List<EventNode>? Node { get; set; }
}

public class EventNode
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("parent")]
    public int Parent { get; set; }

    [JsonPropertyName("_superparent")]
    public int Superparent { get; set; }

    [JsonPropertyName("author")]
    public int Author { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("subtype")]
    public string? Subtype { get; set; }

    [JsonPropertyName("subsubtype")]
    public string? Subsubtype { get; set; }

    [JsonPropertyName("published")]
    public DateTime Published { get; set; }

    [JsonPropertyName("created")]
    public DateTime Created { get; set; }

    [JsonPropertyName("modified")]
    public DateTime Modified { get; set; }

    [JsonPropertyName("_trust")]
    public int Trust { get; set; }

    [JsonPropertyName("version")]
    public int Version { get; set; }

    [JsonPropertyName("slug")]
    public string? Slug { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("body")]
    public string? Body { get; set; }

    [JsonPropertyName("scope")]
    public string? Scope { get; set; }

    [JsonPropertyName("node-timestamp")]
    public DateTime NodeTimestamp { get; set; }

    [JsonPropertyName("meta")]
    public object? Meta { get; set; }

    [JsonPropertyName("meta-timestamp")]
    public DateTime MetaTimestamp { get; set; }

    [JsonPropertyName("path")]
    public string? Path { get; set; }

    [JsonPropertyName("parents")]
    public List<int>? Parents { get; set; }

    [JsonPropertyName("files")]
    public List<object>? Files { get; set; }

    [JsonPropertyName("files-timestamp")]
    public int FilesTimestamp { get; set; }

    [JsonPropertyName("love")]
    public int Love { get; set; }

    [JsonPropertyName("games")]
    public int? Games { get; set; }

    [JsonPropertyName("posts")]
    public int? Posts { get; set; }
}
