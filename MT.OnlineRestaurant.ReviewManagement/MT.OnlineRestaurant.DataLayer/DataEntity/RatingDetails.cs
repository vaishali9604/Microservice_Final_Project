using System;
using System.Collections.Generic;
using System.Text;

namespace MT.OnlineRestaurant.DataLayer.DataEntity
{
    public class RatingDetails
    {
        
        public int Rating { get; set; }
        public  string Reviews { get; set; }
        public string RestaurantName { get; set; }
        public int? CustomerId { get; set; }
        public int RestaurantId { get; set; }
        public int Id { get; set; }
    }
}
