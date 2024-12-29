using UnclewoodCleanArchitecture.Domain.Common.Models;

namespace UnclewoodCleanArchitecture.Domain.Meal.Entities;

public sealed class Photo : Entity
{
    public Photo(
        string url, 
        bool isMain, 
        string publicId, 
        string name, 
        string containerName, 
        Guid? id=null) : base(id ?? Guid.NewGuid())
    {
        Url = url;
        IsMain = isMain;
        PublicId = publicId;
        Name = name;
        ContainerName = containerName;
    }
    
    public string Url { get; private set; }
    
    public bool IsMain { get; private set; }
    
    public string PublicId { get; private set; }
    
    public string Name { get; private set; }
    
    public string ContainerName { get; private set; }
    
    public Guid MealId { get; private set; }
    
    public static Photo Create(
        string url,
        string publicId,
        string name,
        string containerName,
        bool isMain = false)
    {
        ValidatePhotoData(url, publicId, name, containerName);

        return new Photo(
            url,
            isMain,
            publicId,
            name,
            containerName);
    }
    private static void ValidatePhotoData(
        string url,
        string publicId,
        string name,
        string containerName)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            //TODO throw new DomainException("URL cannot be empty");
        }

        if (string.IsNullOrWhiteSpace(publicId))
        {
            //TODO throw new DomainException("Public ID cannot be empty");
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            //TODO throw new DomainException("Name cannot be empty");
        }

        if (string.IsNullOrWhiteSpace(containerName))
        {
            //TODO throw new DomainException("Container name cannot be empty");
        }

        if (!Uri.TryCreate(url, UriKind.Absolute, out _))
        {
            //TODO throw new DomainException("Invalid URL format");
        }
    }
    
    public void SetAsMain()
    {
        IsMain = true;
    }

    public void UnsetAsMain()
    {
        IsMain = false;
    }

    public void UpdateUrl(string newUrl)
    {
        if (string.IsNullOrWhiteSpace(newUrl))
        {
            //TODO throw new DomainException("URL cannot be empty");
        }

        if (!Uri.TryCreate(newUrl, UriKind.Absolute, out _))
        {
            //TODO throw new DomainException("Invalid URL format");
        }

        Url = newUrl;
    }

    public void UpdatePublicId(string newPublicId)
    {
        if (string.IsNullOrWhiteSpace(newPublicId))
        {
            //TODO throw new DomainException("Public ID cannot be empty");
        }

        PublicId = newPublicId;
    }
    
    private Photo() : base(id:Guid.NewGuid())
    { }
}