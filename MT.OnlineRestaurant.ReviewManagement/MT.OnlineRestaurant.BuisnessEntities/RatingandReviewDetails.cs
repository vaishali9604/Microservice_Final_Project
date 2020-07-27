using System;
using System.Collections.Generic;
using System.Text;

namespace MT.OnlineRestaurant.BuisnessEntities
{
    public class RatingandReviewDetails
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Reviews { get; set; }        
        public string RestaurantName { get; set; }
        public int TblCustomerId { get; set; }
        public int TblRestaurantId { get; set; }
       

    }
}
