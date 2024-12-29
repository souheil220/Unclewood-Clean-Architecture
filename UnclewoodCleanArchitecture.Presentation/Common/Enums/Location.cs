using System.Text.Json.Serialization;

namespace UnclewoodCleanArchitectur.Presentation.Common.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Location
{
    SBA,
    ORAN
}