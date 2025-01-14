using UnclewoodCleanArchitecture.Domain.Common.Enum;

namespace UnclewoodCleanArchitecture.Application.DTOS;

public record PriceDto(decimal Value, string Currency, string Location);