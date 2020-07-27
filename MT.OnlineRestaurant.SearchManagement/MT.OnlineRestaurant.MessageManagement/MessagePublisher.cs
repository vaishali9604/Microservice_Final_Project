using Microsoft.Azure.ServiceBus;
using MT.OnlineRestaurant.BusinessEntities;
using MT.OnlineRestaurant.BusinessLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Configuration;

namespace MT.OnlineRestaurant.MessageManagement
{
    public class MessagePublisher : IMessagePublisher
    {
        private readonly IRestaurantBusiness _restaurantBusiness;
        private readonly IConfiguration _configuration;
        private readonly SubscriptionClient _subscriptionClient;
        private const string TOPIC_PATH = "ordersearchtopic";
        private const string SUBSCRIPTION_NAME = "sendcartsubscription";
       
        public MessagePublisher(IConfiguration configuration, IRestaurantBusiness restaurantBusiness)
        {
            _configuration = configuration;
            _restaurantBusiness = restaurantBusiness;
            _subscriptionClient = new SubscriptionClient(
                _configuration.GetConnectionString("ServiceBusConnectionString"),
                TOPIC_PATH,
                SUBSCRIPTION_NAME);
        }
       
        public void RegisterOnMessageHandlerAndReceiveMessages()
        {
            
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };
            _subscriptionClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
        }
        async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            string msg = Encoding.UTF8.GetString(message.Body);

            var stockEntity = JsonConvert.DeserializeObject<StockItems>(Encoding.UTF8.GetString(message.Body));
            StockItems cart = new StockItems
            {
                RestaurantId = stockEntity.RestaurantId,
                MenuId = stockEntity.MenuId,
                CustomerId = stockEntity.CustomerId              
            };

            _restaurantBusiness.ItemInStock(cart.RestaurantId,cart.MenuId,cart.CustomerId);         


            await _subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);

            await _subscriptionClient.CloseAsync();

            
        }

        Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;
            Console.WriteLine("Exception context for troubleshooting:");
            Console.WriteLine($"- Endpoint: {context.Endpoint}");
            Console.WriteLine($"- Entity Path: {context.EntityPath}");
            Console.WriteLine($"- Executing Action: {context.Action}");
            return Task.CompletedTask;
        }
        

        public async Task SendMessageAsync(UpdatePriceEntity updatepricemessage)
        {
            ITopicClient topicClient;
            topicClient = new TopicClient(_configuration.GetConnectionString("ServiceBusConnectionString"), TOPIC_PATH);
            
            string data = JsonConvert.SerializeObject(updatepricemessage);
            Message message = new Message(Encoding.UTF8.GetBytes(data));
            message.UserProperties["messageType"] = typeof(UpdatePriceEntity).Name;

            try
            {
                await topicClient.SendAsync(message);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task SendMessagesAsync(ItemStockMessage restaurantStock)
        {
            ITopicClient topicClient;
            topicClient = new TopicClient(_configuration.GetConnectionString("ServiceBusConnectionString"), TOPIC_PATH);
            ItemStockMessage restaurantstock = new ItemStockMessage
            {
                TblRestaurantId = restaurantStock.TblRestaurantId,
                TblMenuId = restaurantStock.TblMenuId,
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
