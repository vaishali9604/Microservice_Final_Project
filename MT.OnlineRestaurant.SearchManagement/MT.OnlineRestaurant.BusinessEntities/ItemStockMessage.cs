using System;
using System.Collections.Generic;
using System.Text;

namespace MT.OnlineRestaurant.BusinessEntities
{
   public class ItemStockMessage
    {
        
        public string Message { get; set; }         

        
        public int TblCustomerId { get; set; }
        public int TblRestaurantId { get; set; }
        public int TblMenuId { get; set; }
        public int Quantity { get; set; }
        

    }
}
