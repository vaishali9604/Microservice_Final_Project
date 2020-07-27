using System;
using System.Collections.Generic;
using System.Text;

namespace MT.OnlineRestaurant.DataLayer.DataEntity
{
   public class UpdateMenuEntity
    {
        //public int ID { get; set; }
        public int TblRestaurantId { get; set; }
        public int TblMenuId { get; set; }
        public decimal? Price { get; set; }
    }

}
