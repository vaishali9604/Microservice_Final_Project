using FluentValidation;
using MT.OnlineRestaurant.BuisnessEntities;
using MT.OnlineRestaurant.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MT.OnlineRestaurant.ReviewManagement.ModelValidator
{
   
    public class ReviewValidator : AbstractValidator<RatingandReviewDetails>
    {
        private readonly IRestaurantBuisness _restaurantBuisness;
        public ReviewValidator(int UserId, string UserToken,IRestaurantBuisness restaurantBuisness)
        {
            _restaurantBuisness = restaurantBuisness;
                 

            RuleFor(m => m)
                .NotEmpty()
                .NotNull()
                .Must(r => BeAValidRestaurant(r,UserId,UserToken)).When(p => p.TblRestaurantId != 0).WithMessage("Invalid Restaurant");


            RuleFor(m => m.TblCustomerId)
                .NotEmpty()
                .NotNull()
              .Must(BeAValidCustomer).When(p => p.TblCustomerId != 0).WithMessage("Invalid Customer");

            RuleFor(m => m.Reviews)
                .NotEmpty()
                .NotNull()
                .MaximumLength(100).WithMessage("Reviews must be less than 100 characters");

        }

        /// <summary>
        /// Make a service call to fetch all restaurants and validate between them
        /// </summary>
        /// <param name="orderEntity">OrderEntity</param>
        /// <param name="UserId">UserId</param>

        /// <returns>Boolean whether specified restaurant is valid or invalid</returns>
        public bool BeAValidRestaurant(RatingandReviewDetails reviewDetails, int UserId, string UserToken)
        {
            bool IsValidRestaurant = _restaurantBuisness.IsValidRestaurantAsync(reviewDetails,UserId,UserToken).GetAwaiter().GetResult();
            return IsValidRestaurant;
        }
        private bool BeAValidCustomer(int CustomerId)
        {
            bool IsValidCustomer = false;
            if (CustomerId!=0)
            {
                IsValidCustomer = true;
            }
                

            return IsValidCustomer;
        }

    }
}
