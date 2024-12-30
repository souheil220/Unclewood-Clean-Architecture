using MediatR;
using UnclewoodCleanArchitecture.Application.Common.Interfaces;
using UnclewoodCleanArchitecture.Domain.Meal.Events;

namespace UnclewoodCleanArchitecture.Application.Events;

public class MealCreationEventHandler(IMealRepository mealRepository) : INotificationHandler<MealCreatedEvent>
{
    private readonly IMealRepository _mealRepository = mealRepository;

    public async Task Handle(MealCreatedEvent notification, CancellationToken cancellationToken)
    {
        var meal  = await _mealRepository.GetMealByGuidAsync(notification.MealId)
            ?? throw new ApplicationException($"Meal with id {notification.MealId} does not exist.");
        
        // Perform side effects (notifications, logging, etc.)
        // Example: Send a notification about the meal creation
        await SendMealCreatedNotification(meal);
    }
    
    private Task SendMealCreatedNotification(Domain.Meal.Meal meal)
    {
        // Logic to send notification about the meal creation
        return Task.CompletedTask;
    }
}