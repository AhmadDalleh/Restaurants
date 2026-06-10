using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommandValidator : AbstractValidator<UpdateRestaurantCommand>
    {
        private readonly List<string> validCategory = ["Italian", "Chinese", "Mexican", "Indian", "French", "Japanese", "Mediterranean", "Thai", "American", "Spanish"];
        public UpdateRestaurantCommandValidator()
        {
            RuleFor(x => x.Name)
                .Length(3, 100)
                .WithMessage("Name must be between 3 and 100 characters");
            RuleFor(x => x.Description)
                .MaximumLength(500)
                .WithMessage("Description must be less than 500 characters");

            //RuleFor(x => x.Category)
            //    .Must(validCategory.Contains)
            //    .WithMessage($"Category must be one of the following: {string.Join(",", validCategory)}");
            ////.Must(x => validCategory.Contains(x));
            ////.Custom((value, context) =>
            ////{
            ////    var isValidCategory = validCategory.Contains(value);
            ////    if (!isValidCategory)
            ////    {
            ////        context.AddFailure($"Category must be one of the following: {string.Join(", ", validCategory)}");
            ////    }
            ////});

            //RuleFor(dto => dto.ContactEmail)
            //    .EmailAddress()
            //    .WithMessage("ContactEmail must be a valid email address");

            //RuleFor(dto => dto.ContactNumber)
            //    .Matches(@"^\+?[1-9]\d{1,14}$")
            //    .WithMessage("ContactPhone must be a valid phone number");

            //RuleFor(dto => dto.PostalCode)
            //    .Matches(@"^\d{2}(-\d{3})?$")
            //    .WithMessage("PostalCode must be in the format XX-XXX or XXXXX");
        }
    }
}
