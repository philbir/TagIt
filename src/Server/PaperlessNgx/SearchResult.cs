using System.Text.Json.Serialization;

namespace TagIt.PaperlessNgx;

public class Document
{
    [JsonPropertyName("id")] public int Id { get; set; }

    [JsonPropertyName("correspondent")] public object Correspondent { get; set; }

    [JsonPropertyName("document_type")] public object DocumentType { get; set; }

    [JsonPropertyName("storage_path")] public object StoragePath { get; set; }

    [JsonPropertyName("title")] public string Title { get; set; }

    [JsonPropertyName("content")] public string Content { get; set; }

    [JsonPropertyName("tags")] public List<object> Tags { get; set; }

    [JsonPropertyName("created")] public DateTime Created { get; set; }

    [JsonPropertyName("created_date")] public string CreatedDate { get; set; }

    [JsonPropertyName("modified")] public DateTime Modified { get; set; }

    [JsonPropertyName("added")] public DateTime Added { get; set; }

    [JsonPropertyName("archive_serial_number")]
    public object ArchiveSerialNumber { get; set; }

    [JsonPropertyName("original_file_name")]
    public string OriginalFileName { get; set; }

    [JsonPropertyName("archived_file_name")]
    public string ArchivedFileName { get; set; }

    [JsonPropertyName("__search_hit__")] public SearchHit SearchHit { get; set; }
}

public class DocumentSearchResult
{
    [JsonPropertyName("count")] public int Count { get; set; }

    [JsonPropertyName("next")] public object Next { get; set; }

    [JsonPropertyName("previous")] public object Previous { get; set; }

    [JsonPropertyName("results")] public List<Document> Results { get; set; }
}

public class SearchHit
{
    [JsonPropertyName("score")] public double Score { get; set; }

    [JsonPropertyName("highlights")] public string Highlights { get; set; }

    [JsonPropertyName("rank")] public int Rank { get; set; }
}
