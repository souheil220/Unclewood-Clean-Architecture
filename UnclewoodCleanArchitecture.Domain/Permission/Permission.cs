using UnclewoodCleanArchitecture.Domain.Common.ValueObject;
using UnclewoodCleanArchitecture.Domain.Permission.Enum;

namespace UnclewoodCleanArchitecture.Domain.Permission;

public sealed class Permission
{
    public static readonly Permission MealDelete = new(1, Name.Create(nameof(PermissionEnum.MealDelete)));
    public static readonly Permission IngredientRead = new(2, Name.Create(nameof(PermissionEnum.IngredientRead)));
    public static readonly Permission IngredientDelete = new(3, Name.Create(nameof(PermissionEnum.IngredientDelete)));
    public static readonly Permission UsersRead = new(4, Name.Create(nameof(PermissionEnum.UserRead)));
    public static readonly Permission UserDelete = new(5, Name.Create(nameof(PermissionEnum.UserDelete)));
    public static readonly Permission MealAdd = new(6, Name.Create(nameof(PermissionEnum.MealAdd)));
    public static readonly Permission IngredientAdd = new(7, Name.Create(nameof(PermissionEnum.IngredientAdd)));
    public static readonly Permission UserAdd = new(8, Name.Create(nameof(PermissionEnum.UserAdd)));

    private Permission(int id, Name name)
    {
        Id = id;
        Name = name;
    }
    public static Permission Create(int id ,string name)
    {
        return new Permission(id, Name.Create(name));
    }

    public int Id { get; init; }

    public Name Name { get; init; }
}
