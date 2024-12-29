using System.Text.Json.Serialization;

namespace UnclewoodCleanArchitectur.Presentation.Common.Enums;

[Newtonsoft.Json.JsonConverter(typeof(JsonStringEnumConverter))]
public enum Category
{
    Pizza,
    Sandwich,
    Coffee
}