using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MT.OnlineRestaurant.DataLayer.Context;
using MT.OnlineRestaurant.DataLayer.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MT.OnlineRestaurant.DataLayer
{
    public class PlaceOrderDbAccess : IPlaceOrderDbAccess
    {
        private readonly OrderManagementContext _context;

        public PlaceOrderDbAccess(OrderManagementContext context)
        {
            _context = context;
        }

        public int PlaceOrder(TblFoodOrder OrderedFoodDetails)
        {
            _context.TblFoodOrder.Add(OrderedFoodDetails);
            _context.SaveChanges();
            return OrderedFoodDetails.Id;
        }

        public int CancelOrder(int orderId)
        {
            var orderedFood = _context.TblFoodOrder.Include(p => p.TblFoodOrderMapping)
                .SingleOrDefault(p => p.Id == orderId);

            orderedFood.TblFoodOrderMapping.ToList().ForEach(p => _context.TblFoodOrderMapping.Remove(p));
            _context.TblFoodOrder.Remove(orderedFood);
            _context.SaveChanges();
            
            return (orderedFood != null ? orderedFood.Id : 0);
        }

        /// <summary>
        /// gets the customer placed order details
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public IQueryable<TblFoodOrder> GetReports(int customerId)
        {
            return _context.TblFoodOrder.Where(fo => fo.TblCustomerId == customerId);
        }
        

        public int PriceChange(TblCart cartEntity)
        {         
                try
                {
                    var cartprice = _context.TblCart.FirstOrDefault(s => s.TblMenuId.Equals(cartEntity.TblMenuId) && 
                    s.TblRestaurantId.Equals(cartEntity.TblRestaurantId)
                   );

                    if (cartprice != null && cartprice.Id > 0)
                    {
                    cartprice.Price = cartEntity.Price;
                    cartprice.TotalPrice = cartEntity.Price * cartprice.Quantity;
                      _context.SaveChanges();
                    return cartprice.Id;
                }
                return 0;


                }
                catch (Exception e)
                {
                    throw e;
                }
            
            
        }

        public int AddCart(TblCart tblCart)
        {
            tblCart.TotalPrice = tblCart.Price * tblCart.Quantity;

            _context.TblCart.Add(tblCart);
            _context.SaveChanges();
            return tblCart.Id;
        }
        public int UpdateCart(TblCart updateCart)
        {
            
            var updatecartitems = _context.TblCart.Where(item => item.TblCustomerId.Equals(updateCart.TblCustomerId) &&
            item.TblRestaurantId.Equals(updateCart.TblMenuId) && item.TblRestaurantId.Equals(updateCart.TblRestaurantId)).FirstOrDefault();
            updatecartitems.TblRestaurantId = updateCart.TblRestaurantId;
            updatecartitems.TblCustomerId = updateCart.TblCustomerId;
            updatecartitems.TblMenuId = updateCart.TblMenuId;
            updatecartitems.Message = updateCart.Message;           
            _context.SaveChanges();
            return updateCart.Id;
        }



    }
}
