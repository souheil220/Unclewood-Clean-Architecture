using AutoMapper;
using UnclewoodCleanArchitecture.Application.DTOS;
using UnclewoodCleanArchitecture.Domain.Common.Entities;
using UnclewoodCleanArchitecture.Domain.Meal.Entities;
using UnclewoodCleanArchitecture.Domain.Meal.ValueObjects;

namespace UnclewoodCleanArchitecture.Application.Helper;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        /*CreateMap<Domain.Ingredient.Ingredient, IngrediantDTO>();
        CreateMap<Domain.Meal.Meal, MealResponseDTO>()
            .ForMember(dest => dest.Ingrediants,
                opt => opt.MapFrom(src => src.MealIngrediants
                    .Select(mi => mi.Ingrediant)))
            .ForMember(dest => dest.Photos,
                opt => opt.MapFrom(src => src.Photos
                    .Select(p => new PhotoDTO { Url = p.Url })));
        CreateMap<MealDTO, Domain.Meal.Meal>();
        CreateMap<Domain.Meal.Meal, MealDTO>();*/
        CreateMap<Photo, PhotoDto>().ReverseMap();
        CreateMap<Price, PriceDto>().ReverseMap();
        CreateMap<MealIngredient, MealIngredientDto>().ReverseMap();

    }
}