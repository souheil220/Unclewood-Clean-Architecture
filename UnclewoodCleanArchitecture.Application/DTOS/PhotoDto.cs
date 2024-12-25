namespace UnclewoodCleanArchitecture.Application.DTOS;

public record PhotoDto(string Url,
    string Name,
    string ContainerName,
    bool IsMain);