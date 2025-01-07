using MediatR;
using UnclewoodCleanArchitecture.Application.Common.Interfaces;
using UnclewoodCleanArchitecture.Application.Common.Interfaces.Query;
using UnclewoodCleanArchitecture.Application.Data;
using UnclewoodCleanArchitecture.Domain.Common;
using UnclewoodCleanArchitecture.Domain.Meal.Errors;

namespace UnclewoodCleanArchitecture.Application.Meal.Queries.GetMeal;

public class GetMealQueryHandler(IMealRepository mealRepository,ISqlConnectionFactory sqlConnectionFactory) : IQueryHandler<GetMealQuery, Domain.Meal.Meal>
{
    public async Task<Result<Domain.Meal.Meal>> Handle(GetMealQuery request, CancellationToken cancellationToken)
    {
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
       

        //TODO Use Dapper here so you can query you Meal table and populate your ingredients
          var meal = await mealRepository.GetMealByGuidAsync(request.MealId);
          if (meal is null)
          {
              return Result.Failure<Domain.Meal.Meal>(MealErrors.NotFound);
          }
          meal.RaiseMealsListed();
          return Result.Success(meal);
    }
}