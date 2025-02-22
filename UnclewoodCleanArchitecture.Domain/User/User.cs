using UnclewoodCleanArchitecture.Domain.Common.Models;
using UnclewoodCleanArchitecture.Domain.Common.ValueObject;
using UnclewoodCleanArchitecture.Domain.Role.Enum;
using UnclewoodCleanArchitecture.Domain.User.Events;

namespace UnclewoodCleanArchitecture.Domain.User;

public class User : AggregateRoot
{
    public User(
        Guid id ,
        Name firstName, 
        Name lastName, 
        Email email
        ) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        
    }
    private User() :base(Guid.NewGuid()){}

    public Name FirstName { get; private set; }
    public Name LastName { get; private set; }
    public Email Email { get; private set; }
    public string IdentityId { get; private set; } = string.Empty;

    private readonly List<Role.Role> _roles = new();


    public IReadOnlyCollection<Role.Role> Roles => _roles.ToList();

    public static User Create(Name firstName, Name lastName, Email email, List<string> roles)
    {
        var user = new User(Guid.NewGuid(), firstName, lastName, email);

        user.RaiseDomainEvent(new UserCreatedEvent(user.Id));

        foreach (var role in roles)
        {
            var newRole = EnumToRole(role);
            user._roles.Add(newRole);
        }

        return user;
    }
    

    public void SetIdentityId(string identityId)
    {
        IdentityId = identityId;
    }
    
    private static Role.Role EnumToRole(string role)
    {
         Enum.TryParse<Roles>(role, out Roles result);
        return Role.Role.Create((int)result,role);
    }
    
}