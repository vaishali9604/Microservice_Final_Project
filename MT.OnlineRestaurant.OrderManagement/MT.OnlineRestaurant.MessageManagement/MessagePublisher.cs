
using Microsoft.Azure.ServiceBus;
using MT.OnlineRestaurant.BusinessEntities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using MT.OnlineRestaurant.BusinessLayer.interfaces;
using Microsoft.Extensions.Configuration;

namespace MT.OnlineRestaurant.MessageManagement
{
    public class MessagePublisher : IMessagePublisher
    {
       

        private readonly IPlaceOrderActions _orderBusiness;
        private readonly IConfiguration _configuration;
        private readonly SubscriptionClient _subscriptionClient;
        private const string TOPIC_PATH = "ordersearchtopic";
        private const string SUBSCRIPTION_NAME = "pricechangesubscription";
        public MessagePublisher(IPlaceOrderActions orderBusiness,
            IConfiguration configuration)
        {
            _orderBusiness = orderBusiness;
            _configuration = configuration;          

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
            var cartentity = JsonConvert.DeserializeObject<CartEntity>(Encoding.UTF8.GetString(message.Body));
            CartEntity cart = new CartEntity {
                TblRestaurantId = cartentity.TblRestaurantId,
                TblMenuId = cartentity.TblMenuId,
                Price = cartentity.Price
            };
           
            _orderBusiness.UpdatePriceChange(cart);           
            
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
       
        public async Task SendMessagetoSearchAsync(OrderEntity orderEntity)
        {
            OrderQuantity orderQuantity = new OrderQuantity();
            
            foreach( var menu in orderEntity.OrderMenuDetails)
            {
                orderQuantity.MenuId=menu.MenuId.Value;
            }
             orderQuantity.RestaurantId= orderEntity.RestaurantId;
             orderQuantity.CustomerId=orderEntity.CustomerId ;
            ITopicClient topicClient;
            topicClient = new TopicClient(_configuration.GetConnectionString("ServiceBusConnectionString"), TOPIC_PATH);
            string data = JsonConvert.SerializeObject(orderQuantity);
            Message message = new Message(Encoding.UTF8.GetBytes(data));
            
            message.UserProperties["messageType"] = typeof(OrderQuantity).Name;
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
