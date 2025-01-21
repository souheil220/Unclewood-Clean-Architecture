using AutoMapper;
using UnclewoodCleanArchitecture.Application.DTOS;
using UnclewoodCleanArchitecture.Application.Ingredient;
using UnclewoodCleanArchitecture.Domain.Common.Entities;
using UnclewoodCleanArchitecture.Domain.Common.Enum;
using UnclewoodCleanArchitecture.Domain.Meal.Entities;
using UnclewoodCleanArchitecture.Domain.Meal.ValueObjects;

namespace UnclewoodCleanArchitecture.Application.Helper;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Photo, PhotoDto>().ReverseMap();
        CreateMap<Price, PriceDto>().ReverseMap();
        CreateMap<PriceDto, Price>()
            .ForCtorParam("value", opt 
                => opt.MapFrom(src => src.Value)) // Map constructor parameter
            .ForCtorParam("currency", opt
                => opt.MapFrom(src => src.Currency)) // Map constructor parameter
            .ForCtorParam("location", opt 
                => opt.MapFrom(src => Location.FromName(src.Location,false)));

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
        
    
      
        CreateMap<MealIngredient, MealIngredientDto>()
            .ForMember(dest => dest.IngredientId,
                opt
                    => opt.MapFrom(src => src.IngredientId))
            .ForMember(dest => dest.IngredientName,
                opt 
                    => opt.MapFrom(src => src.Ingredient.Name.Value))
            .ReverseMap();

    }
}