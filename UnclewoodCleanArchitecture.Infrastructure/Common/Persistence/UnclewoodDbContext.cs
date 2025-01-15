using System.Data;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using UnclewoodCleanArchitecture.Application.Common.Interfaces;
using UnclewoodCleanArchitecture.Domain.Common.Entities;
using UnclewoodCleanArchitecture.Domain.Common.Models;
using UnclewoodCleanArchitecture.Infrastructure.Clock;
using UnclewoodCleanArchitecture.Infrastructure.Outbox;

namespace UnclewoodCleanArchitecture.Infrastructure.Common.Persistence;

public class UnclewoodDbContext(DbContextOptions options, IDateTimeProvider dateTimeProvider) : DbContext(options), IUnitOfWork
{
    private static readonly JsonSerializerSettings JsonSerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.All
    };

    public DbSet<Domain.Meal.Meal> Meals { get; set; } = null!;
    public DbSet<Domain.Ingredient.Ingredient> Ingredients { get; set; } = null!;
    
    public DbSet<Domain.User.User> Users { get; set; } = null!;
    
    public DbSet<Domain.Role.Role> Roles { get; set; } = null!;
    
    public DbSet<OutboxMessage> OutboxMessages { get; set; } = null!;
    
    public DbSet<MealIngredient> MealIngrediants { get; set; }
    
    public DbSet<Domain.Common.Entities.RolePermission> RolePermission { get; set; }
    

    public async Task CommitChangesAsync()
    {
        try
        {
            PublishDomainEventsAsync();
            await SaveChangesAsync();

        }
        catch (DbUpdateConcurrencyException ex)
        {
           
            throw new DBConcurrencyException(ex.Message);
        }   
       
    }

    private void PublishDomainEventsAsync()
    {
        var outboxMessages = ChangeTracker
            .Entries<Entity>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                var domainEvents = entity.GetDomainEvents();

                entity.ClearDomainEvents();

                return domainEvents;
            })
            .Select(domainEvent => new OutboxMessage(
                Guid.NewGuid(),
                dateTimeProvider.UtcNow,
                domainEvent.GetType().Name,
                JsonConvert.SerializeObject(domainEvent, JsonSerializerSettings)))
            .ToList();

        AddRange(outboxMessages);
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