using UnclewoodCleanArchitectur.Presentation.Common.Enums;

namespace UnclewoodCleanArchitectur.Presentation.DTOs;

public record IngredientDto(
    Guid Id,
    string Name,
    List<Location> DisponibleIn,
    decimal Price);
