using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using UnclewoodCleanArchitecture.Application.Common.Interfaces;
using UnclewoodCleanArchitecture.Domain.Common.Entities;
using UnclewoodCleanArchitecture.Domain.Common.Events;
using UnclewoodCleanArchitecture.Domain.Common.Models;

namespace UnclewoodCleanArchitecture.Infrastructure.Common.Persistence;

public class UnclewoodDbContext(DbContextOptions options,IHttpContextAccessor httpContextAccessor) : DbContext(options), IUnitOfWork
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    public DbSet<Domain.Meal.Meal> Meals { get; set; } = null!;
    public DbSet<Domain.Ingredient.Ingredient> Ingredients { get; set; } = null!;
    
    public DbSet<MealIngredient> MealIngrediants { get; set; }


    public async Task CommitChangesAsync()
    {
        var domainEvents = ChangeTracker.Entries<Entity>()
                                .Select(entry => entry.Entity.PopDomainEvents())
                                .SelectMany(events => events)
                                .ToList();

        AddDomainEventToOffLineProcessingQueue(domainEvents);
        await SaveChangesAsync();
    }

    private void AddDomainEventToOffLineProcessingQueue(List<IDomainEvent> domainEvents)
    {
        var domainEventsQueue =httpContextAccessor.HttpContext!.Items.
                                            TryGetValue("DomainEventQueue" , out var value) 
                                                && value is Queue<IDomainEvent> existingDomainEvents? existingDomainEvents
                                            :new Queue<IDomainEvent>();
        domainEvents.ForEach(domainEventsQueue.Enqueue);
        
        httpContextAccessor.HttpContext!.Items["DomainEventQueue"] = domainEventsQueue;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
  
        modelBuilder.Entity<MealIngredient>()
            .HasKey(mi => new { mi.MealId, mi.IngredientId });
        
        modelBuilder.Entity<MealIngredient>()
            .HasOne(mi => mi.Meal)
            .WithMany(m => m.MealIngredients)
            .HasForeignKey(mi => mi.MealId);

        modelBuilder.Entity<MealIngredient>()
            .HasOne(mi => mi.Ingredient)
            .WithMany(i => i.MealIngrediants)
            .HasForeignKey(mi => mi.IngredientId);
    }
}