using AutoMapper;
using UnclewoodCleanArchitecture.Application.DTOS;
using UnclewoodCleanArchitecture.Application.Ingredient;
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

        CreateMap<Domain.Ingredient.Ingredient, IngredientResponse>()
            .ForMember(dest => dest.Name,
                opt
                    => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.DisponibleIn,
                opt 
                    => opt.MapFrom(src 
                        => src.DisponibleIn.Select(l => l.Name)))
            .ForMember(dest => dest.Price,
                opt 
                    => opt.MapFrom(src => src.Price.Value));
        
    
      
        CreateMap<MealIngredient, MealIngredientDto>().ReverseMap();

    }
}