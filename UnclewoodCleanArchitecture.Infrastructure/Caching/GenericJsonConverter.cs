using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace UnclewoodCleanArchitecture.Infrastructure.Caching;

public class GenericJsonConverter<T> : JsonConverter<T>
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
        {
            var root = doc.RootElement;
            
            // Create a dictionary of property values
            var propertyValues = new Dictionary<string, object>();
            
            foreach (var property in typeof(T).GetProperties())
            {
                var propertyName = GetJsonPropertyName(property);
                if (root.TryGetProperty(propertyName, out var value))
                {
                    var propertyValue = JsonSerializer.Deserialize(value.GetRawText(), property.PropertyType, options);
                    if (propertyValue != null)
                    {
                        propertyValues[property.Name] = propertyValue;
                    }
                }
            }

            // Create the instance using object initialization
            var instance = (T)Activator.CreateInstance(typeof(T), Array.Empty<object>())!;
            
            // Use reflection to set the init-only properties
            var flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty;
            foreach (var kvp in propertyValues)
            {
                var property = typeof(T).GetProperty(kvp.Key, flags);
                if (property != null)
                {
                    property.SetValue(instance, kvp.Value, null);
                }
            }

            return instance;
        }
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        if (value == null)
        {
            writer.WriteNullValue();
            return;
        }

        writer.WriteStartObject();

        var properties = typeof(T).GetProperties()
            .Where(p => p.GetCustomAttribute<JsonIgnoreAttribute>() == null);

        foreach (var property in properties)
        {
            var propertyValue = property.GetValue(value);
            if (propertyValue != null)
            {
                var propertyName = GetJsonPropertyName(property);
                writer.WritePropertyName(propertyName);
                JsonSerializer.Serialize(writer, propertyValue, property.PropertyType, options);
            }
        }

        writer.WriteEndObject();
    }

    private static string GetJsonPropertyName(PropertyInfo property)
    {
        var jsonPropertyAttribute = property.GetCustomAttribute<JsonPropertyNameAttribute>();
        return jsonPropertyAttribute?.Name ?? property.Name;
    }
}