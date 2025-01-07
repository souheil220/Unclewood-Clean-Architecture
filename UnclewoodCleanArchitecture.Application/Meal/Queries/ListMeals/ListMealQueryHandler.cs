using AutoMapper;
using Dapper;
using UnclewoodCleanArchitecture.Application.Common.Interfaces;
using UnclewoodCleanArchitecture.Application.Common.Interfaces.Query;
using UnclewoodCleanArchitecture.Application.Data;
using UnclewoodCleanArchitecture.Application.Meal.Queries.GetMeal;
using UnclewoodCleanArchitecture.Domain.Common;

namespace UnclewoodCleanArchitecture.Application.Meal.Queries.ListMeals;

public class ListMealQueryHandler(IMealRepository mealRepository,ISqlConnectionFactory mealConnectionFactory)
    : IQueryHandler<ListMealQuery, IEnumerable<Domain.Meal.Meal>>
{

    public async Task<Result<IEnumerable<Domain.Meal.Meal>>> Handle(ListMealQuery request, CancellationToken cancellationToken)
    {
      /*  using var connection = _sqlConnectionFactory.CreateConnection();
        var sql = $"""
                   WITH MealPhotos AS (
                       SELECT p."MealId", JSONB_AGG(to_jsonb(p) - 'Id' ) AS photos
                       FROM "Photo" AS p
                       GROUP BY p."MealId"
                   ),
                   MealPrices AS (
                       SELECT p0."MealId", JSONB_AGG(to_jsonb(p0) - 'MealId' -'Id') AS prices
                       FROM "Price" AS p0
                       GROUP BY p0."MealId"
                   ),
                   MealIngredients AS (
                       SELECT m0."MealId", JSONB_AGG(
                           JSONB_BUILD_OBJECT(
                                'Ingredient', to_jsonb(i) - 'Id' - 'IngredientId' - 'Price_Value', -- Remove Id and IngredientId from Ingredient
                               'Location', to_jsonb(l) - 'Id' - 'IngredientId' - 'DisponibleInValue'
                           )
                       ) AS ingredients
                       FROM "MealIngrediants" AS m0
                       INNER JOIN "Ingredients" AS i ON m0."IngredientId" = i."Id"
                       LEFT JOIN "Location" AS l ON i."Id" = l."IngredientId"
                       GROUP BY m0."MealId"
                   )
                   SELECT m."Id", m."BestSeller", m."Category", m."Description", m."Name", m."Promotion", m."PromotionRate",
                          COALESCE(mp.photos, '[]'::jsonb) AS photos,
                          COALESCE(mpr.prices, '[]'::jsonb) AS prices,
                          COALESCE(mi.ingredients, '[]'::jsonb) AS ingredients
                   FROM "Meals" AS m
                   LEFT JOIN MealPhotos AS mp ON m."Id" = mp."MealId"
                   LEFT JOIN MealPrices AS mpr ON m."Id" = mpr."MealId"
                   LEFT JOIN MealIngredients AS mi ON m."Id" = mi."MealId";
                   """;
        
        var mealDapper = await connection.QueryAsync<MealResponse,PhotoResponse,PriceResponse,IngredientsResponse,MealResponse>(
            sql,
            (meal,photos, price, ingredient) =>
            {
                meal.Price = price;
                meal.Photos = photos;
                meal.Ingredients = ingredient;
                return meal;
            },
            
            splitOn: "MealDetail"
            
        );
        return mealDapper;*/
        var meals = await mealRepository.GetMealsAsync();
        return Result.Success(meals);
    }
}