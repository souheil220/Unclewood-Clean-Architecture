using System.Text.Json.Serialization;
using UnclewoodCleanArchitecture.Domain.Common.ValueObject;
using UnclewoodCleanArchitecture.Domain.Role.Enum;
using UnclewoodCleanArchitecture.Domain.User;

namespace UnclewoodCleanArchitecture.Domain.Role;

public sealed class Role
{
    public static readonly Role Manager = new(2, Name.Create(nameof(Roles.Manager)));
    public static readonly Role Admin = new(1, Name.Create(nameof(Roles.Admin)));
    [JsonConstructor]
    public Role(int id, Name name)
    {
        Id = id;
        Name = name;
    }

    public static Role Create(int id ,string name)
    {
        return new Role(id, Name.Create(name));
    }

    public int Id { get; init; }

    public Name Name { get; init; }

    public ICollection<User.User> Users { get; init; } = new List<User.User>();
    

    private Role(){}
}
