using FluentValidation;
using Restaurants.Application.Restaurants.Dtos;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants;

public class GetAllRestaurantsQueryValidator : AbstractValidator<GetAllRestaurantsQuery>
{
    private readonly int[] allowedPageSizes = { 5, 10, 15, 30 };

    private readonly string[] allowedSortByColumns = [nameof(RestaurantDto.Name), nameof(RestaurantDto.Description), nameof(RestaurantDto.Category)];
    public GetAllRestaurantsQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(x => x.PageSize)
            .Must(x => allowedPageSizes.Contains(x))
            .WithMessage($"Page size must be in [{string.Join(",", allowedPageSizes)}]");

        RuleFor(r => r.SortBy)
            .Must(value => allowedSortByColumns.Contains(value))
            .When(q => q.SortBy != null)
            .WithMessage($"Sort By optional, or must be in [{string.Join(",", allowedSortByColumns)}]");
    }
}
