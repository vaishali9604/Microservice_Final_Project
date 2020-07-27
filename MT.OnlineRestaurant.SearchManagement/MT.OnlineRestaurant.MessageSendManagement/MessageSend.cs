using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using MT.OnlineRestaurant.BusinessEntities;
using System.Threading.Tasks;

namespace MT.OnlineRestaurant.MessageSendManagement
{
    public class MessageSend : IMessageSend
    {
        private readonly IConfiguration _configuration;
        private readonly SubscriptionClient _subscriptionClient;
        private const string TOPIC_PATH = "ordersearchtopic";
        private const string SUBSCRIPTION_NAME = "itemoutofstocksubscription";

        public MessageSend(IConfiguration configuration)
        {
            _configuration = configuration;

            _subscriptionClient = new SubscriptionClient(
                _configuration.GetConnectionString("ServiceBusConnectionString"),
                TOPIC_PATH,
                SUBSCRIPTION_NAME);
        }
        public async Task SendMessagesAsync(ItemStockMessage restaurantStock)
            {
            ITopicClient topicClient;
            topicClient = new TopicClient(_configuration.GetConnectionString("ServiceBusConnectionString"), TOPIC_PATH);
            ItemStockMessage restaurantstock = new ItemStockMessage
            {
                TblRestaurantId = restaurantStock.TblRestaurantId,
                TblMenuId = restaurantStock.TblMenuId,
                TblCustomerId=restaurantStock.TblCustomerId,
                Message = "Item Out of Stock"
            };
            string data = JsonConvert.SerializeObject(restaurantstock);
            Message message = new Message(Encoding.UTF8.GetBytes(data));
            message.UserProperties["messageType"] = typeof(ItemStockMessage).Name;
            try
            {
                await topicClient.SendAsync(message);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        
    }
}
