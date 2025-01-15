using System.Text.Json.Serialization;
using Ardalis.SmartEnum;

namespace UnclewoodCleanArchitecture.Domain.Common.Enum;


public class Location : SmartEnum<Location>
{
    public static readonly Location SBA= new(nameof(SBA), 0);
    public static readonly Location ORAN  = new(nameof(ORAN), 1);
    [JsonConstructor]
    public Location(string name, int value) : base(name, value)
    {
    }
    
}