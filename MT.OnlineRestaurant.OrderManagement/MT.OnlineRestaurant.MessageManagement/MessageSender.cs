using Microsoft.Azure.ServiceBus;
using MT.OnlineRestaurant.BusinessEntities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MT.OnlineRestaurant.BusinessLayer;
using MT.OnlineRestaurant.BusinessLayer.interfaces;
using System.Threading;

namespace MT.OnlineRestaurant.MessageManagement
{
    public class MessageSender : IMessageSender
    {
        private readonly IPlaceOrderActions _orderBusiness;
        private readonly IConfiguration _configuration;
        private readonly SubscriptionClient _subscriptionClient;
        private const string TOPIC_PATH = "ordersearchtopic";         
        private const string SUBSCRIPTION_NM = "itemoutofstocksubscription";
        public MessageSender(IConfiguration configuration,IPlaceOrderActions orderBuisness)
        {
            _configuration = configuration;
            _orderBusiness = orderBuisness;
            _subscriptionClient = new SubscriptionClient(
                _configuration.GetConnectionString("ServiceBusConnectionString"),
                TOPIC_PATH,
                SUBSCRIPTION_NM);
        }
        public void RegisterOnMessageHandlerforCartMessages()
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };
            _subscriptionClient.RegisterMessageHandler(ProcessMessagescartAsync, messageHandlerOptions);
        }
        async Task ProcessMessagescartAsync(Message message, CancellationToken token)
        {
            string msg = Encoding.UTF8.GetString(message.Body);
            var cartitemsentity = JsonConvert.DeserializeObject<CartEntity>(Encoding.UTF8.GetString(message.Body));
            CartEntity cart = new CartEntity
            {
                TblRestaurantId = cartitemsentity.TblRestaurantId,
                TblMenuId = cartitemsentity.TblMenuId,
                TblCustomerId = cartitemsentity.TblCustomerId,
                Price = cartitemsentity.Price,
                Message = cartitemsentity.Message

            };
            _orderBusiness.UpdateCart(cart);
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
              
    }
}
