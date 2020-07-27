using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MT.OnlineRestaurant.BuisnessEntities;
using MT.OnlineRestaurant.BusinessLayer;
using MT.OnlineRestaurant.ReviewManagement.Controller;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MT.OnlineRestaurant.Test.Controller
{
    public class ReviewControllerTest
    {
        [Test]
        public void GetReviewsandRatings()
        {
            List<RatingandReviewDetails> restaurantratingDetails = new List<RatingandReviewDetails>() ;
            string RestaurantName = "Frotinar";
            restaurantratingDetails.Add(new RatingandReviewDetails()
            {
                Rating = 1,
                Reviews = "egreddior fecit. travissimantor estis pars plorum quo cognitio, estum. Sed habitatio Multum et vantis.",
                RestaurantName = "Frotinar",
                Id =3 ,
                TblCustomerId =3,
                TblRestaurantId =3 });
            var mockOrder = new Mock<IRestaurantBuisness>();
            mockOrder.Setup(x => x.GetRestaurantRating(RestaurantName)).Returns(restaurantratingDetails.AsQueryable());
            var search = new ReviewController(mockOrder.Object);
            var data = search.GetResturantRatingDetail(RestaurantName);

            var okObjectResult = data as OkObjectResult;
            Assert.AreEqual(200, okObjectResult.StatusCode);
            Assert.IsNotNull(okObjectResult);
        }
       
        
        [Test]
        public async Task PostReviewandRatings()
        {
            
            RatingandReviewDetails ratingandreviewsentity = new RatingandReviewDetails
            {
                Rating = 2,
                Reviews = "very good",
                TblCustomerId = 1
                

            };
            var mockOrder = new Mock<IRestaurantBuisness>();
            mockOrder.Setup(x => x.PostRatingandReviews(ratingandreviewsentity)).Returns(true);
            var postreviews = new ReviewController(mockOrder.Object);
            postreviews.ControllerContext = new ControllerContext();
            postreviews.ControllerContext.HttpContext = new DefaultHttpContext();
            postreviews.ControllerContext.HttpContext.Request.Headers["CustomerId"]="1";
            postreviews.ControllerContext.HttpContext.Request.Headers["AuthToken"] = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJuYmYiOjE1OTU3OTIwMjcsImV4cCI6MTU5NTc5MzgyNywiaWF0IjoxNTk1NzkyMDI3LCJpc3MiOiJNMTA0MzAyN0BtaW5kdHJlZS5jb20ifQ._uVEF3Y-4NJqmxXXh60f5dAFZb1TYoxn34LsyUuXdjw";
            var data = await postreviews.Post(ratingandreviewsentity);
            var okObjectResult = data as OkObjectResult;
            Assert.AreEqual(200, okObjectResult.StatusCode);
        }

        [Test]
        public async Task UpdateReviewandRatings()
        {
            
            RatingandReviewDetails ratingandreviewsentity = new RatingandReviewDetails
            {
                Rating = 2,
                Reviews = "very good",
                TblCustomerId = 1,
                TblRestaurantId=1
      

            };
            var mockOrder = new Mock<IRestaurantBuisness>();
            mockOrder.Setup(x => x.UpdateRatingandReviews(ratingandreviewsentity)).Returns(true);
            
            var updatereviews = new ReviewController(mockOrder.Object);
            updatereviews.ControllerContext = new ControllerContext();
            updatereviews.ControllerContext.HttpContext = new DefaultHttpContext();
            updatereviews.ControllerContext.HttpContext.Request.Headers["CustomerId"] = "1";
            updatereviews.ControllerContext.HttpContext.Request.Headers["AuthToken"] = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJuYmYiOjE1OTU3OTIwMjcsImV4cCI6MTU5NTc5MzgyNywiaWF0IjoxNTk1NzkyMDI3LCJpc3MiOiJNMTA0MzAyN0BtaW5kdHJlZS5jb20ifQ._uVEF3Y-4NJqmxXXh60f5dAFZb1TYoxn34LsyUuXdjw";
            mockOrder.Setup(x => x.IsValidRestaurantAsync(ratingandreviewsentity, 1 , updatereviews.ControllerContext.HttpContext.Request.Headers["AuthToken"])).ReturnsAsync(true);
            var data = await updatereviews.Put(ratingandreviewsentity);
            var okObjectResult = data as OkObjectResult;
            Assert.AreEqual(200, okObjectResult.StatusCode);
        }

        [Test]        
        public async Task PostReviews_Invalid()
        {
            RatingandReviewDetails ratingandreviewsentity = new RatingandReviewDetails
            {
                Rating =0 ,
                Reviews = "",
                TblCustomerId = 1,
                TblRestaurantId=1

            };

            var mockOrder = new Mock<IRestaurantBuisness>();
            mockOrder.Setup(x => x.PostRatingandReviews(ratingandreviewsentity)).Returns(true);
            var postreviews = new ReviewController(mockOrder.Object);
            postreviews.ControllerContext = new ControllerContext();
            postreviews.ControllerContext.HttpContext = new DefaultHttpContext();
            postreviews.ControllerContext.HttpContext.Request.Headers["CustomerId"] = "1";
            postreviews.ControllerContext.HttpContext.Request.Headers["AuthToken"] = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJuYmYiOjE1OTU1MjIwNDYsImV4cCI6MTU5NTUyMzg0NiwiaWF0IjoxNTk1NTIyMDQ2LCJpc3MiOiJNMTA0MzAyN0BtaW5kdHJlZS5jb20ifQ.Vz94haCPJEXw88laXihE4TFfUQPoJfH9spGD_Cnnhsc";
            mockOrder.Setup(x => x.IsValidRestaurantAsync(ratingandreviewsentity, 1, postreviews.ControllerContext.HttpContext.Request.Headers["AuthToken"])).ReturnsAsync(true);
            var data = await postreviews.Post(ratingandreviewsentity);

            var okObjectResult = data as BadRequestObjectResult;
            Assert.AreEqual(400, okObjectResult.StatusCode);
        }

        [Test]
        public async Task UpdateReviews_Invalid()
        {
            RatingandReviewDetails ratingandreviewsentity = new RatingandReviewDetails
            {
                Rating = 0,
                Reviews = "",
                TblCustomerId = 1

            };

            var mockOrder = new Mock<IRestaurantBuisness>();
            mockOrder.Setup(x => x.PostRatingandReviews(ratingandreviewsentity)).Returns(true);
            var updatereviews = new ReviewController(mockOrder.Object);
            updatereviews.ControllerContext = new ControllerContext();
            updatereviews.ControllerContext.HttpContext = new DefaultHttpContext();
            updatereviews.ControllerContext.HttpContext.Request.Headers["CustomerId"] = "1";
            updatereviews.ControllerContext.HttpContext.Request.Headers["AuthToken"] = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJuYmYiOjE1OTU1MjIwNDYsImV4cCI6MTU5NTUyMzg0NiwiaWF0IjoxNTk1NTIyMDQ2LCJpc3MiOiJNMTA0MzAyN0BtaW5kdHJlZS5jb20ifQ.Vz94haCPJEXw88laXihE4TFfUQPoJfH9spGD_Cnnhsc";
            mockOrder.Setup(x => x.IsValidRestaurantAsync(ratingandreviewsentity, 1, updatereviews.ControllerContext.HttpContext.Request.Headers["AuthToken"])).ReturnsAsync(true);
            var data = await updatereviews.Put(ratingandreviewsentity);
            var okObjectResult = data as BadRequestObjectResult;
            Assert.AreEqual(400, okObjectResult.StatusCode);
        }

    }
}
