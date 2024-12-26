using System.Reflection;
using Microsoft.EntityFrameworkCore;
using UnclewoodCleanArchitecture.Application.Common.Interfaces;
using UnclewoodCleanArchitecture.Domain.Common.Entities;
using UnclewoodCleanArchitecture.Domain.Ingredient;
using UnclewoodCleanArchitecture.Domain.Meal;

namespace UnclewoodCleanArchitectur.Infrastructure.Common.Persistence;

public class UnclewoodDbContext : DbContext, IUnitOfWork
{
    public DbSet<Meal> Admins { get; set; } = null!;
    public DbSet<Ingredient> Subscriptions { get; set; } = null!;
    public DbSet<MealIngredient> MealIngrediants { get; set; }


    public UnclewoodDbContext(DbContextOptions options) : base(options)
    {
    }

    public async Task CommitChangesAsync()
    {
        await SaveChangesAsync();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}