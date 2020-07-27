using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http;

using MT.OnlineRestaurant.BuisnessEntities;
using MT.OnlineRestaurant.BusinessLayer;
using MT.OnlineRestaurant.ReviewManagement.ModelValidator;
using FluentValidation.Results;

namespace MT.OnlineRestaurant.ReviewManagement.Controller
{
    [Produces("application/json")]
    [Route("api")]
    public class ReviewController : ControllerBase
    {
        private readonly IRestaurantBuisness business_Repo;
        public ReviewController(IRestaurantBuisness _business_Repo)
        {
            business_Repo = _business_Repo;
        }

        [HttpGet]
        [Route("RestaurantRatings")]
        public IActionResult GetResturantRatingDetail([FromQuery] string RestaurantName)
        {
            IQueryable<RatingandReviewDetails> restaurantratingDetails;
            restaurantratingDetails = business_Repo.GetRestaurantRating(RestaurantName);
            if (restaurantratingDetails != null)
            {
                return this.Ok(restaurantratingDetails);
            }
            return this.StatusCode((int)HttpStatusCode.InternalServerError, string.Empty);
        }

        [HttpPost]
        [Route("PostRatingandReviews")]
        public async Task<IActionResult> Post([FromBody] RatingandReviewDetails ratingandReviewDetails)
        {
            int UserId = (Request.Headers.ContainsKey("CustomerId") ? int.Parse(HttpContext.Request.Headers["CustomerId"]) : 0);
            string UserToken = (Request.Headers.ContainsKey("AuthToken") ? Convert.ToString(HttpContext.Request.Headers["AuthToken"]) : "");
            ReviewValidator reviewEntityValidator = new ReviewValidator(UserId,UserToken,business_Repo);
            ValidationResult validationResult = reviewEntityValidator.Validate(ratingandReviewDetails);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ToString(";"));
            }
            else
            {
                var result = await Task<bool>.Run(() => business_Repo.PostRatingandReviews(ratingandReviewDetails));
                if (result.Equals("false"))
                {
                    return this.BadRequest("Failed to add , please try again later");
                }
            }
            return this.Ok("Added");

        }

        [HttpPut]
        [Route("UpdateRatingandReviews")]
        public async Task<IActionResult> Put([FromBody] RatingandReviewDetails ratingandReviewDetails)
        {
            int UserId = (Request.Headers.ContainsKey("CustomerId") ? int.Parse(HttpContext.Request.Headers["CustomerId"]) : 0);
            string UserToken = (Request.Headers.ContainsKey("AuthToken") ? Convert.ToString(HttpContext.Request.Headers["AuthToken"]) : "");
            ReviewValidator reviewEntityValidator = new ReviewValidator(UserId, UserToken, business_Repo);
            ValidationResult validationResult = reviewEntityValidator.Validate(ratingandReviewDetails);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ToString(";"));
            }
            else
            {
                var result = await Task<bool>.Run(() => business_Repo.UpdateRatingandReviews(ratingandReviewDetails));
                if (result.Equals("false"))
                {
                    return this.BadRequest("Failed to update , please try again later");
                }
            }
            return this.Ok("Updated");
            

        }
    }
}
