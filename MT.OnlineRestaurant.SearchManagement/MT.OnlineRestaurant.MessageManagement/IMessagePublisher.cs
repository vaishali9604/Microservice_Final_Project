using MT.OnlineRestaurant.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MT.OnlineRestaurant.MessageManagement
{
    public interface IMessagePublisher
    {
        void RegisterOnMessageHandlerAndReceiveMessages();
       
        Task SendMessageAsync(UpdatePriceEntity updatepricemessage);
        Task SendMessagesAsync(ItemStockMessage restaurantStock);

    }
}
