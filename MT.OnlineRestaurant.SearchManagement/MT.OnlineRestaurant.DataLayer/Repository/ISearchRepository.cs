
using MT.OnlineRestaurant.DataLayer.DataEntity;
using MT.OnlineRestaurant.DataLayer.EntityFrameWorkModel;
using System.Collections.Generic;
using System.Linq;

namespace MT.OnlineRestaurant.DataLayer.Repository
{
    public interface ISearchRepository
    {
        TblRestaurant GetResturantDetails(int restaurantID);
        IQueryable<TblRating> GetRestaurantRating(int restaurantID);

        IQueryable<MenuDetails> GetRestaurantMenu(int restaurantID);

        IQueryable<TblRestaurantDetails> GetTableDetails(int restaurantID);
        IQueryable<RestaurantSearchDetails> GetRestaurantsBasedOnLocation(LocationDetails location_Details);
        IQueryable<RestaurantSearchDetails> GetRestaurantsBasedOnMenu(AddtitionalFeatureForSearch searchDetails);
        IQueryable<RestaurantSearchDetails> SearchForRestaurant(SearchForRestautrant searchDetails);

        IQueryable<RestaurantSearchDetails> SearchForRestaurantBasedOnMultipleFeactures(MultipleSearchFeature multiplesearchDetails);

        /// <summary>
        /// Recording the customer rating the restaurants
        /// </summary>
        /// <param name="tblRating"></param>C:\Users\vmadmin\Desktop\301-Restaurant-Project\MT.OnlineRestaurant.SearchManagement\MT.OnlineRestaurant.DataLayer\EntityFrameWorkModel\
        void RestaurantRating(TblRating tblRating);
        TblMenu ItemInStock(int restaurantID,int MenuID);
        TblMenu ItemOutOfStock(int restaurantID, int menuID);
        decimal UpdateMenuPrice(TblOffer menuprice);
        int UpdateFromReceivedMessage(int restaurantid);

    }
}
