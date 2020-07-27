
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using MT.OnlineRestaurant.BusinessLayer;
using MT.OnlineRestaurant.DataLayer.DataEntity;
using MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel;
using MT.OnlineRestaurant.DataLayer.Repository;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MT.OnlineRestaurant.Test.DataLayer
{
    [TestFixture]
    public class ReviewRepositoryTest
    {
        [Test]
        public void PostReviewsandRatings()
        {
            TblRatingandReviews ratingandReviews = new TblRatingandReviews()
            {
                TblRestaurantId = 10,
                TblCustomerId = 1,
                Rating = 3,
                Reviews = "It was awsome"

            };
            var options = new DbContextOptionsBuilder<RestaurantManagementContext>()
                .UseInMemoryDatabase(databaseName: "ReviewManagement")
                .Options;
            ReviewRepository reviewRepository = new ReviewRepository(new RestaurantManagementContext(options));
            bool isupdated = reviewRepository.PostRatingandReviews(ratingandReviews);

            Assert.IsTrue(isupdated);

        }


        


        [Test]
        public void GetReviewsandRatings()
        {

            var options = new DbContextOptionsBuilder<RestaurantManagementContext>()
                .UseInMemoryDatabase(databaseName: "ReviewManagement")
                .Options;
            RestaurantManagementContext context = new RestaurantManagementContext(options);
            TblRatingandReviews ratingandReviews = new TblRatingandReviews
            {
                Id = 1,
                Rating = 5,
                Reviews = "Amazing",
                TblRestaurantId = 3

            };
            context.TblRatingandReviews.Add(ratingandReviews);
            TblRestaurant tblRestaurant = new TblRestaurant
            {
                Id = 3,
                Name = "Frotinar"

            };
            context.TblRestaurant.Add(tblRestaurant);
            ReviewRepository reviewRepository = new ReviewRepository(context);
            var restaurantratings = reviewRepository.GetRestaurantRating("Frotinar");

            Assert.NotNull(restaurantratings);
        }

    }
}