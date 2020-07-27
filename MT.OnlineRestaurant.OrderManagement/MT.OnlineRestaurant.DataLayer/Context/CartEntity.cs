using System;
using System.Collections.Generic;
using System.Text;

namespace MT.OnlineRestaurant.DataLayer.Context
{
    public class CartEntity
    {
        public int Id { get; set; }
        public int TblCustomerId { get; set; }
        public int TblRestaurantId { get; set; }
        public int TblMenuId { get; set; }
        public int Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? TotalPrice { get; set; }
    }
}
