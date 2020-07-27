using Moq;
using MT.OnlineRestaurant.BuisnessEntities;
using MT.OnlineRestaurant.BusinessLayer;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using MT.OnlineRestaurant.DataLayer;
using MT.OnlineRestaurant.DataLayer.Repository;
using MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel;


namespace MT.OnlineRestaurant.Test.BuisnessLayer
{
    public class BuisnessLayerTest
    {
        [Test]
        public void PostReviewandRatings()
        {
           
            RatingandReviewDetails ratingandreviewsentity = new RatingandReviewDetails
            {
                Rating = 2,
                Reviews = "very good",
                TblCustomerId = 1
            };
           
            var mockOrder = new Mock<IReviewRespository>();            
            mockOrder.Setup(x =>x.PostRatingandReviews(It.IsAny<TblRatingandReviews>())).Returns(true);
            var ReviewObject = new RestaurantBuisness(mockOrder.Object);
            var data = ReviewObject.PostRatingandReviews(ratingandreviewsentity);

            Assert.AreEqual(true, data);
        }

        [Test]
        public void UpdateReviewandRatings()
        {
            
            RatingandReviewDetails ratingandreviewsentity = new RatingandReviewDetails
            {
                Rating = 2,
                Reviews = "very good",
                TblCustomerId = 1
            
            
            };
           
            var mockOrder = new Mock<IReviewRespository>();           
            mockOrder.Setup(x => x.UpdateRatingandReviews(It.IsAny<TblRatingandReviews>())).Returns(true);
            var ReviewObject = new RestaurantBuisness(mockOrder.Object);
            var data = ReviewObject.UpdateRatingandReviews(ratingandreviewsentity);

            Assert.AreEqual(true, data);
        }

        [Test]
        public void UpdateReviewandRatings_InValid()
        {
           
            RatingandReviewDetails ratingandreviewsentity = new RatingandReviewDetails
            {
                Rating = 0,
                Reviews = "",
                TblCustomerId = 0,
                Id=0

            };

            var mockOrder = new Mock<IReviewRespository>();            
            mockOrder.Setup(x => x.UpdateRatingandReviews(It.IsAny<TblRatingandReviews>())).Returns(false);
            var ReviewObject = new RestaurantBuisness(mockOrder.Object);
            var data = ReviewObject.UpdateRatingandReviews(ratingandreviewsentity);

            Assert.AreEqual(false, data);
        }
    }
}
