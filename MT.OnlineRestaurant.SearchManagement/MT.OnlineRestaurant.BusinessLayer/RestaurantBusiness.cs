﻿using MT.OnlineRestaurant.BusinessEntities;
using System;
using System.Collections.Generic;
using MT.OnlineRestaurant.DataLayer.Repository;
using MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel;
using MT.OnlineRestaurant.DataLayer.DataEntity;
using System.Text;
using System.Linq;
using AutoMapper;
using MT.OnlineRestaurant.MessageSendManagement;


namespace MT.OnlineRestaurant.BusinessLayer
{
    public class RestaurantBusiness : IRestaurantBusiness
    {
        ISearchRepository search_Repository;
        private readonly IMessageSend messageSend;
        private readonly IMapper mapper;
        private readonly string connectionstring;
        
        
        public RestaurantBusiness(ISearchRepository _searchRepository,IMapper _mapper,IMessageSend _messageSend)
        {
            search_Repository = _searchRepository;
            mapper = _mapper;
            messageSend = _messageSend;
        }

        public IQueryable<RestaurantMenu> GetRestaurantMenus(int restaurantID)
        {
            IQueryable<MenuDetails> menuDetails;
            List<RestaurantMenu> restaurant_Menu = new List<RestaurantMenu>();
            try
            {
                menuDetails = search_Repository.GetRestaurantMenu(restaurantID);
                foreach (var menu in menuDetails)
                {
                    RestaurantMenu menuInfo = new RestaurantMenu
                    {
                        menu_ID = menu.tbl_Offer.Id,
                        dish_Name = menu.tbl_Menu.Item,
                        price = menu.tbl_Offer.Price,
                        running_Offer = menu.tbl_Offer.Discount,
                        cuisine = menu.tbl_Cuisine.Cuisine
                    };
                    restaurant_Menu.Add(menuInfo);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return restaurant_Menu.AsQueryable();
        }

        public IQueryable<RestaurantRating> GetRestaurantRating(int restaurantID)
        {
            try
            {
                List<RestaurantRating> restaurantRatings = new List<RestaurantRating>();
                IQueryable<TblRating> rating;
                rating = search_Repository.GetRestaurantRating(restaurantID);
                foreach (var item in rating)
                {
                    RestaurantRating ratings = new RestaurantRating
                    {
                        rating = item.Rating,
                        RestaurantId = item.TblRestaurantId,
                        user_Comments = item.Comments,
                        customerId = item.TblCustomerId,
                    };
                    restaurantRatings.Add(ratings);
                }
                return restaurantRatings.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public RestaurantInformation GetResturantDetails(int restaurantID)
        {
            try
            {
                TblRestaurant restaurant = new TblRestaurant();
                restaurant = search_Repository.GetResturantDetails(restaurantID);
                RestaurantInformation resturant_Information = new RestaurantInformation
                {
                    restaurant_ID = restaurant.Id,
                    restaurant_Name = restaurant.Name,
                    restaurant_Address = restaurant.Address,
                    restaurant_ContactNo = restaurant.ContactNo,
                    closing_Time = restaurant.CloseTime,
                    opening_Time = restaurant.OpeningTime,
                    website = restaurant.Website,
                    xaxis = (double)restaurant.TblLocation.X,
                    yaxis = (double)restaurant.TblLocation.Y
                };
                return resturant_Information;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<RestaurantTables> GetTableDetails(int restaurantID)
        {
            try
            {
                List<RestaurantTables> TableInfo = new List<RestaurantTables>();
                IQueryable<TblRestaurantDetails> restaurantTableCount;
                restaurantTableCount = search_Repository.GetTableDetails(restaurantID);
                foreach (var item in restaurantTableCount)
                {
                    RestaurantTables table = new RestaurantTables
                    {
                        restaurant_Name = item.TblRestaurant.Name,
                        table_Capacity = item.TableCapacity,
                        total_Count = item.TableCount
                    };
                    TableInfo.Add(table);
                }
                return TableInfo.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<RestaurantInformation> SearchRestaurantByLocation(BusinessEntities.LocationDetails locationDetails)
        {
            try
            {
                List<RestaurantInformation> restaurant_Info = new List<RestaurantInformation>();
                IQueryable<RestaurantSearchDetails> searched_Restaurant;
                DataLayer.DataEntity.LocationDetails location_Details = new DataLayer.DataEntity.LocationDetails
                {
                    distance = locationDetails.distance,
                    restaurant_Name = locationDetails.restaurant_Name,
                    xaxis = locationDetails.xaxis,
                    yaxis = locationDetails.yaxis
                };

                searched_Restaurant = search_Repository.GetRestaurantsBasedOnLocation(location_Details);
                foreach (var restaurants in searched_Restaurant)
                {
                    RestaurantInformation restaurant_Details = new RestaurantInformation
                    {
                        restaurant_ID = restaurants.restauran_ID,
                        restaurant_Name = restaurants.restaurant_Name,
                        restaurant_Address = restaurants.restaurant_Address,
                        restaurant_ContactNo = restaurants.restaurant_PhoneNumber,
                        closing_Time = restaurants.closing_Time,
                        opening_Time = restaurants.opening_Time,
                        website = restaurants.restraurant_Website,
                        xaxis = restaurants.xaxis,
                        yaxis = restaurants.yaxis
                    };
                    restaurant_Info.Add(restaurant_Details);
                }
                return restaurant_Info.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<RestaurantInformation> GetRestaurantsBasedOnMenu(AdditionalFeatureForSearch additionalFeatureForSearch)
        {
            try
            {
                List<RestaurantInformation> restaurant_Info = new List<RestaurantInformation>();
                IQueryable<RestaurantSearchDetails> searched_Restaurant;
                DataLayer.DataEntity.AddtitionalFeatureForSearch searchCritera = new DataLayer.DataEntity.AddtitionalFeatureForSearch
                {
                    cuisine = (string.IsNullOrEmpty(additionalFeatureForSearch.cuisine) ? "" : additionalFeatureForSearch.cuisine),
                    Menu = (string.IsNullOrEmpty(additionalFeatureForSearch.Menu) ? "" : additionalFeatureForSearch.Menu),
                    rating = (additionalFeatureForSearch.rating != null && additionalFeatureForSearch.rating > 0) ? additionalFeatureForSearch.rating : 0
                };

                searched_Restaurant = search_Repository.GetRestaurantsBasedOnMenu(searchCritera);
                if (searched_Restaurant != null)
                {
                    foreach (var restaurants in searched_Restaurant)
                    {
                        RestaurantInformation restaurant_Details = new RestaurantInformation
                        {
                            restaurant_ID = restaurants.restauran_ID,
                            restaurant_Name = restaurants.restaurant_Name,
                            restaurant_Address = restaurants.restaurant_Address,
                            restaurant_ContactNo = restaurants.restaurant_PhoneNumber,
                            closing_Time = restaurants.closing_Time,
                            opening_Time = restaurants.opening_Time,
                            website = restaurants.restraurant_Website,
                            xaxis = restaurants.xaxis,
                            yaxis = restaurants.yaxis
                        };
                        restaurant_Info.Add(restaurant_Details);
                    }
                }
                return restaurant_Info.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<RestaurantInformation> SearchForRestaurant(SearchForRestaurant searchDetails)
        {
            throw new NotImplementedException();
        }

        public IQueryable<RestaurantInformation> SearchForRestaurantBasedOnMultipleFeactures(BusinessEntities.MultipleSearchFeature multiplesearchDetails)
        {
            try
            {
                List<RestaurantInformation> restaurant_Info = new List<RestaurantInformation>();
                IQueryable<RestaurantSearchDetails> searched_Restaurant;
                DataLayer.DataEntity.MultipleSearchFeature searchCritera = new DataLayer.DataEntity.MultipleSearchFeature
                {
                    cuisine = (string.IsNullOrEmpty(multiplesearchDetails.cuisine) ? "" : multiplesearchDetails.cuisine),
                    Menu = (string.IsNullOrEmpty(multiplesearchDetails.Menu) ? "" : multiplesearchDetails.Menu),
                    rating = (multiplesearchDetails.rating != null && multiplesearchDetails.rating > 0) ? multiplesearchDetails.rating : 0,
                    RestaurantName = (string.IsNullOrEmpty(multiplesearchDetails.RestaurantName) ? "" : multiplesearchDetails.RestaurantName)
                };
                searched_Restaurant = search_Repository.SearchForRestaurantBasedOnMultipleFeactures(searchCritera);
                foreach (var restaurants in searched_Restaurant)
                {
                    RestaurantInformation restaurant_Details = new RestaurantInformation
                    {
                        restaurant_ID = restaurants.restauran_ID,
                        restaurant_Name = restaurants.restaurant_Name,
                        restaurant_Address = restaurants.restaurant_Address,
                        restaurant_ContactNo = restaurants.restaurant_PhoneNumber,
                        closing_Time = restaurants.closing_Time,
                        opening_Time = restaurants.opening_Time,
                        website = restaurants.restraurant_Website,
                        xaxis = restaurants.xaxis,
                        yaxis = restaurants.yaxis
                    };
                    restaurant_Info.Add(restaurant_Details);
                }
                return restaurant_Info.AsQueryable();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        /// <summary>
        /// Recording the customer rating the restaurants
        /// </summary>
        /// <param name=""></param>
        public void RestaurantRating(RestaurantRating restaurantRating)
        {
            if (restaurantRating != null)
            {
                TblRating rating = new TblRating()
                {
                    Rating = restaurantRating.rating,
                    TblRestaurantId = restaurantRating.RestaurantId,
                    Comments = restaurantRating.user_Comments,
                    TblCustomerId = restaurantRating.customerId
                };

                search_Repository.RestaurantRating(rating);
            }
        }
        public int ItemInStock(int restaurantID,int menuID,int customerID)
        {
            RestaurantMenu menuObj = new RestaurantMenu();
            TblMenu menu = search_Repository.ItemInStock(restaurantID,menuID);
             menuObj.quantity = menu.quantity;

            ItemStockMessage restaurantStock = new ItemStockMessage
            {
                TblRestaurantId = restaurantID,
                TblMenuId = menuID,
                TblCustomerId=customerID,
                Message = "Item Out of Stock"
            };
            if(menuObj.quantity==0)
            {
                messageSend.SendMessagesAsync(restaurantStock);
            }
             return menuObj.quantity;
        }
        public int ItemOutOfStock(ItemStockMessage stock)
        {
            RestaurantMenu menuObj = new RestaurantMenu();
            DataLayer.DataEntity.Stockitems _stock = new DataLayer.DataEntity.Stockitems()
            {
                RestaurantId = stock.TblRestaurantId,
                CustomerId=stock.TblCustomerId,
                MenuId = stock.TblMenuId

            };
            TblMenu menu = search_Repository.ItemOutOfStock(_stock.RestaurantId,_stock.MenuId);
            menuObj.quantity = menu.quantity;

            ItemStockMessage restaurantStock = new ItemStockMessage
            {
                TblRestaurantId = stock.TblRestaurantId,
                TblMenuId = stock.TblMenuId,
                TblCustomerId = stock.TblCustomerId,
                Message = "Item Out of Stock"
            };
            if (menuObj.quantity == 0)
            {
                messageSend.SendMessagesAsync(restaurantStock);
            }
            return menuObj.quantity;
        }
        public decimal UpdateItemPrice(UpdatePriceEntity updatePrice)
        {
            DataLayer.EntityFrameWorkModel.TblOffer tblOffer = mapper.Map<DataLayer.EntityFrameWorkModel.TblOffer>(updatePrice);
            try
            {
                if (updatePrice != null)
                {
                   
                    return search_Repository.UpdateMenuPrice(tblOffer);
                }
                else
                {

                    return 0;
                }

            }

            catch (Exception e)
            {
                throw e;
            }
        }
       public int UpdateFromReceivedMessage(string restaurantid)
        {
            return search_Repository.UpdateFromReceivedMessage(Convert.ToInt32(restaurantid));
        }

    }
}
