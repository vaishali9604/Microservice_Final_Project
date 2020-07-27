using Microsoft.AspNetCore.Mvc;
using Moq;
using MT.OnlineRestaurant.BusinessEntities;
using MT.OnlineRestaurant.BusinessLayer;
using MT.OnlineRestaurant.SearchManagement.Controllers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MT.OnlineRestaurant.UT.ControllerTest
{
    public class SearchControllerTest
    {
        [Test]
        public void SearchRestaurantBasedMultipleFactors()
        {
            IQueryable<RestaurantInformation> restaurant_Info = null;
            MultipleSearchFeature multiplesearch = new MultipleSearchFeature
            {
                cuisine = "Florida",
                Menu = "Dosa",
                rating = 1
                
            };

            var mockOrder = new Mock<IRestaurantBusiness>();
            mockOrder.Setup(x => x.SearchForRestaurantBasedOnMultipleFeactures(multiplesearch)).Returns(restaurant_Info.AsQueryable());
            var search = new SearchController(mockOrder.Object);
            var data = search.SearchRestaurantBasedMultipleFactors(multiplesearch);

            var okObjectResult = data as OkObjectResult;
            Assert.AreEqual(200, okObjectResult.StatusCode);
        }


    }
}
