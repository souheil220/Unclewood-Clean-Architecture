using AutoMapper;
using MediatR;
using UnclewoodCleanArchitecture.Application.Common.Interfaces;
using UnclewoodCleanArchitecture.Domain.Meal.Entities;

namespace UnclewoodCleanArchitecture.Application.Meal.Commands.CreateMeal;

public class CreateMealCommandHandler : IRequestHandler<CreateMealCommand, Domain.Meal.Meal>
{
    private readonly IMealRepository _mealRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;



    public CreateMealCommandHandler(IMealRepository mealRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _mealRepository = mealRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        
        
    }
    public async Task<Domain.Meal.Meal> Handle(CreateMealCommand request, CancellationToken cancellationToken)
    {
       

        var meal = new Domain.Meal.Meal(
            name: request.Name, 
            prices: request.Prices,
            description: request.Description,
            bestSeller: request.BestSeller,
            promotion: request.Promotion,
            promotionRate:request.PromotionRate,
            category: request.Category
            );
        var photos = _mapper.Map<List<Photo>>(request.MealPictures);
        meal.AddIngredients(request.IngrediantsIDs);
        meal.AddPhotos(photos);
        await _mealRepository.AddMealAsync(meal);
        await _unitOfWork.CommitChangesAsync();

        return meal;
    }
}