using System;
using System.Collections.Generic;
using System.Text;

namespace MT.OnlineRestaurant.BusinessEntities
{
   public class OrderQuantity
    {
        public int RestaurantId { get; set; }
        public int CustomerId { get; set; }
        public int MenuId { get; set; }
    }
}
