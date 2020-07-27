#region References
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using MT.OnlineRestaurant.BusinessEntities;
using MT.OnlineRestaurant.BusinessLayer.interfaces;
using MT.OnlineRestaurant.OrderAPI.ModelValidators;
using System;
using System.Linq;
using System.Threading.Tasks;

using LoggingManagement;
using Microsoft.Azure.ServiceBus;
using System.Text;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights;
using MT.OnlineRestaurant.MessageManagement;
#endregion

#region Namespace
namespace MT.OnlineRestaurant.OrderAPI.Controllers
{
    /// <summary>
    /// Food ordering controller
    /// </summary>
    [Produces("application/json")]
    public class OrderFoodController : Controller
    {
        private readonly IPlaceOrderActions _placeOrderActions;
        private readonly ILogService _logService;
        private readonly IMessagePublisher _messagePublisher;
        /// <summary>
        /// Inject buisiness layer dependency
        /// </summary>
        /// <param name="placeOrderActions">Instance of this interface is injected in startup</param>
        public OrderFoodController(IPlaceOrderActions placeOrderActions, ILogService logService, IMessagePublisher messagePublisher)
        {
            _placeOrderActions = placeOrderActions;
            _logService = logService;
            _messagePublisher = messagePublisher;
        }
        /// <summary>
        /// POST api/OrderFood
        /// To order food
        /// </summary>
        /// <param name="orderEntity">Order entity</param>
        /// <returns>Status of order</returns>
        [HttpPost]
        [Route("api/OrderFood")]
        public async Task<IActionResult> Post([FromBody] OrderEntity orderEntity)
        {
            _logService.LogMessage("Order Entity received at endpoint : api/OrderFood, User ID : " + orderEntity.CustomerId);
            int UserId = (Request.Headers.ContainsKey("CustomerId") ? int.Parse(HttpContext.Request.Headers["CustomerId"]) : 0);
            string UserToken = (Request.Headers.ContainsKey("AuthToken") ? Convert.ToString(HttpContext.Request.Headers["AuthToken"]) : "");

            OrderEntityValidator orderEntityValidator = new OrderEntityValidator(UserId, UserToken, _placeOrderActions);
            ValidationResult validationResult = orderEntityValidator.Validate(orderEntity);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.ToString("; "));
            }
            else
            {
                var result = await Task<int>.Run(() => _placeOrderActions.PlaceOrder(orderEntity));
                if (result == 0)
                {
                    return BadRequest("Failed to place order, Please try again later");
                }
            }
            await _messagePublisher.SendMessagetoSearchAsync(orderEntity);
            return Ok("Order placed successfully");
        }

        
        /// <summary>
        /// DELETE api/CancelOrder
        /// Cancel the order
        /// </summary>
        /// <param name="id">Order id</param>
        /// <returns>Status of order</returns>
        [HttpDelete]
        [Route("api/CancelOrder")]
        public IActionResult Delete(int id)
        {
            var result = _placeOrderActions.CancelOrder(id);
            if (result > 0)
            {
                return Ok("Order cancelled successfully");
            }

            return BadRequest("Failed to cancel order, Please try again later");
        }
              


        [HttpPut]
        [Route("api/UpdateCart")]
        public async Task<IActionResult> UpdateCart([FromBody] CartEntity cartEntity)
        {
            int UserId = (Request.Headers.ContainsKey("CustomerId") ? int.Parse(HttpContext.Request.Headers["CustomerId"]) : 0);
            cartEntity.TblCustomerId = UserId;
            var result = await Task<int>.Run(() => _placeOrderActions.UpdateCart(cartEntity));
            if (result != 0)
            {
               
                return Ok("Item Updated to Cart");
            }
            else
            {
                return BadRequest("Item not added to the Cart");
            }
        }



        //[HttpGet]
        //[Route("api/Reports")]
        //public IActionResult Reports(int customerId)
        //{
        //    IQueryable<CustomerOrderReport> result = _placeOrderActions.GetReports(customerId);
        //    if(result.Any())
        //    {
        //        return Ok(result.ToList());
        //    }

        //    return BadRequest("Failed to get the reports");
        //}     
    }
}
#endregion