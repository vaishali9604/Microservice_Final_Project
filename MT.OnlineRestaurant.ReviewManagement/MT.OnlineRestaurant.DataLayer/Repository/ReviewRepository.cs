using MT.OnlineRestaurant.DataLayer.DataEntity;
using MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MT.OnlineRestaurant.DataLayer.Repository
{
    public class ReviewRepository : IReviewRespository
    {
        private readonly RestaurantManagementContext db;
        public ReviewRepository(RestaurantManagementContext connection)
        {
            db = connection;
        }
        public IQueryable<RatingDetails> GetRestaurantRating(string RestaurantName)
        {

            try
            {

                if (db != null)
                {

                    return (from rating in db.TblRatingandReviews
                            join restaurant in db.TblRestaurant
                            on rating.TblRestaurantId equals restaurant.Id
                            where restaurant.Name == RestaurantName
                            select new RatingDetails
                            {
                                Rating = rating.Rating,
                                Reviews = rating.Reviews,
                                RestaurantName = restaurant.Name,
                                Id = rating.Id,
                                CustomerId = rating.TblCustomerId.Value,
                                RestaurantId = rating.TblRestaurantId

                            }
                             ).AsQueryable();
                }
                return null;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public bool PostRatingandReviews(TblRatingandReviews ratingandreviews)
        {            
            try
            {
                if (ratingandreviews != null)
                {

                    db.Set<TblRatingandReviews>().Add(ratingandreviews);
                    db.SaveChanges();
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
                
        public bool UpdateRatingandReviews(TblRatingandReviews ratingandreviews)
        {
            try
            {
                var ratingandReviews = db.TblRatingandReviews.FirstOrDefault(s => s.Id.Equals(ratingandreviews.Id));

                if (ratingandReviews != null && ratingandReviews.Id > 0)
                {
                    ratingandReviews.Rating = ratingandreviews.Rating;
                    ratingandReviews.Reviews = ratingandreviews.Reviews;
                    ratingandReviews.RecordTimeStamp = DateTime.Now;
                    ratingandReviews.UserCreated = ratingandreviews.TblCustomerId.HasValue ? ratingandreviews.TblCustomerId.Value : 1;
                    ratingandReviews.UserModified = ratingandreviews.TblCustomerId.HasValue ? ratingandreviews.TblCustomerId.Value : 1;
                    db.SaveChanges();
                    return true;
                }
                return false;


            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

       

    }

    }

