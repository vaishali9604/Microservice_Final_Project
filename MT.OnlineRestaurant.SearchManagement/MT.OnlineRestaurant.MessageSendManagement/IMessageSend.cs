using MT.OnlineRestaurant.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MT.OnlineRestaurant.MessageSendManagement
{
   public interface IMessageSend
    {
        Task  SendMessagesAsync(ItemStockMessage restaurantStock);
    }
}
