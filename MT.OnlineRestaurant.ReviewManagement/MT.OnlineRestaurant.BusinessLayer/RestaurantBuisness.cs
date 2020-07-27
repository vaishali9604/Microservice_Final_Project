using AutoMapper;
using Microsoft.Extensions.Options;
using MT.OnlineRestaurant.BuisnessEntities;
using MT.OnlineRestaurant.BuisnessEntities.SeviceModels;
using MT.OnlineRestaurant.DataLayer;
using MT.OnlineRestaurant.DataLayer.DataEntity;
using MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel;
using MT.OnlineRestaurant.DataLayer.Repository;
using MT.OnlineRestaurant.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MT.OnlineRestaurant.BusinessLayer
{
    public class RestaurantBuisness : IRestaurantBuisness
    {
        private readonly IReviewRespository review_Repository;
        private readonly IMapper _mapper;
       
        private readonly IOptions<ConnectionStrings> _connectionStrings;
        public RestaurantBuisness(IReviewRespository _reviewRepository)
        {
            review_Repository = _reviewRepository;
        }
        public RestaurantBuisness(IReviewRespository _reviewRepository, IMapper mapper, IOptions<ConnectionStrings> connectionStrings)
        {
            review_Repository = _reviewRepository;
            _mapper = mapper;
            _connectionStrings = connectionStrings;
        }

       
        public IQueryable<RatingandReviewDetails> GetRestaurantRating(string restaurantName)
        {
            IQueryable<RatingDetails> ratingDetails;
            List<RatingandReviewDetails> ratingandreviews = new List<RatingandReviewDetails>();
            try
            {
                ratingDetails = review_Repository.GetRestaurantRating(restaurantName);
                foreach (var rating in ratingDetails)
                {
                    RatingandReviewDetails ratingandreview = new RatingandReviewDetails
                    {
                        Rating = rating.Rating,
                        Reviews = rating.Reviews,
                        RestaurantName = rating.RestaurantName,                      
                        Id = rating.Id,
                        TblCustomerId=rating.CustomerId.Value,
                        TblRestaurantId = rating.RestaurantId

                    };
                    ratingandreviews.Add(ratingandreview);
                }
                return ratingandreviews.AsQueryable();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool PostRatingandReviews(RatingandReviewDetails ratingEntity)
        {
            try
            {
                TblRatingandReviews tblRatingandReviews;
                if (ratingEntity!=null)
                {
                    tblRatingandReviews = new TblRatingandReviews();
                    tblRatingandReviews.TblRestaurantId = ratingEntity.TblRestaurantId;
                    tblRatingandReviews.TblCustomerId = ratingEntity.TblCustomerId;
                    tblRatingandReviews.Rating = ratingEntity.Rating;
                    tblRatingandReviews.Reviews = ratingEntity.Reviews;
                    tblRatingandReviews.RecordTimeStampCreated = DateTime.Now;
                    return review_Repository.PostRatingandReviews(tblRatingandReviews);

                }
                return false;

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public bool UpdateRatingandReviews(RatingandReviewDetails ratingEntity)
        {
            try
            {
                TblRatingandReviews tblRatingandReviews;
                if (ratingEntity != null)
                {
                    tblRatingandReviews = new TblRatingandReviews();
                    tblRatingandReviews.TblRestaurantId = ratingEntity.TblRestaurantId;
                    tblRatingandReviews.TblCustomerId = ratingEntity.TblCustomerId;
                    tblRatingandReviews.Rating = ratingEntity.Rating;
                    tblRatingandReviews.Reviews = ratingEntity.Reviews;
                    tblRatingandReviews.RecordTimeStampCreated = DateTime.Now;
                    tblRatingandReviews.Id = ratingEntity.Id;

                    return review_Repository.UpdateRatingandReviews(tblRatingandReviews);

                }
                return false;

            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public async Task<bool> IsValidRestaurantAsync(RatingandReviewDetails ratingEntity, int UserId, string UserToken)
        {
            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("http://localhost:10603/");
            //HttpResponseMessage httpResponseMessage = await client.GetAsync("api/ResturantDetail?RestaurantID=" + ratingEntity.TblRestaurantId);
            //if(httpResponseMessage.IsSuccessStatusCode)
            //{
            //    string json = await httpResponseMessage.Content.ReadAsStringAsync();
            //    RestaurantInformation restaurantInformation = JsonConvert.DeserializeObject<RestaurantInformation>(json);
            //    if(restaurantInformation!=null)
            //    {
            //        return true;
            //    }
            //}
            //return false;
            using (HttpClient httpClient = WebAPIClient.GetClient(UserToken, UserId, "http://localhost:10603/"))
            {
                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync("api/ResturantDetail?RestaurantID=" + ratingEntity.TblRestaurantId);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string json = await httpResponseMessage.Content.ReadAsStringAsync();
                    RestaurantInformation restaurantInformation = JsonConvert.DeserializeObject<RestaurantInformation>(json);
                    if (restaurantInformation != null)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool UpdateRatingandReview(RatingandReviewDetails ratingEntity)
        {
            throw new NotImplementedException();
        }
    }
}
