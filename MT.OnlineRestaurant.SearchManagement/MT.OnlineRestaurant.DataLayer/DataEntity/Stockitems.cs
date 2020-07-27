using System;
using System.Collections.Generic;
using System.Text;

namespace MT.OnlineRestaurant.DataLayer.DataEntity
{
    public class Stockitems
    {
        public int RestaurantId { get; set; }
        public int Quantity { get; set; }
        public int MenuId { get; set; }
        public int CustomerId { get; set; }
    }
}
