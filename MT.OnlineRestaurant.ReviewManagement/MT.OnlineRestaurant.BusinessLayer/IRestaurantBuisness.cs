using MT.OnlineRestaurant.BuisnessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.OnlineRestaurant.BusinessLayer
{
    public interface IRestaurantBuisness
    {
        IQueryable<RatingandReviewDetails> GetRestaurantRating(string restaurantName);      
        bool PostRatingandReviews(RatingandReviewDetails ratingEntity);
        bool UpdateRatingandReviews(RatingandReviewDetails ratingEntity);
        Task<bool> IsValidRestaurantAsync(RatingandReviewDetails ratingEntity, int UserId, string UserToken);
    }
}
