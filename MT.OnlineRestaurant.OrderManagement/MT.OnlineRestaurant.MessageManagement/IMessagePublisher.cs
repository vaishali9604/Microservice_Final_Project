using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MT.OnlineRestaurant.BusinessEntities;

namespace MT.OnlineRestaurant.MessageManagement
{
    public interface IMessagePublisher
    {
        void RegisterOnMessageHandlerAndReceiveMessages();
        
        
        //Task SendMessageAsync(CartEntity cartEntity);
        Task SendMessagetoSearchAsync(OrderEntity orderEntity);
        
    }
}
