using MT.OnlineRestaurant.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT.OnlineRestaurant.DataLayer.interfaces
{
    public interface IPlaceOrderDbAccess
    {
        int PlaceOrder(TblFoodOrder foodOrderDetails);

        int CancelOrder(int orderId);

        /// <summary>
        /// gets the customer placed order details
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        IQueryable<TblFoodOrder> GetReports(int customerId);
        int  PriceChange(TblCart cartEntity);

        int AddCart(TblCart tblCart);

        int UpdateCart(TblCart updateCart);



    }
}
