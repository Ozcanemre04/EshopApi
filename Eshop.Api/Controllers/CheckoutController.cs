using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Application.Dtos.Response.BasketProduct;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;

namespace Eshop.Api.Controllers
{
    [Route("create-checkout-session")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public CheckoutController(IConfiguration configuration)
        {
             _configuration = configuration;
        }
        [HttpPost]
        public ActionResult Create([FromBody] List<BasketProductDtoResponse> items)

        {
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
            try
            {
                var domain = _configuration["Url:Domain"];
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string> { "card" },
                    LineItems = items.Select(item => new SessionLineItemOptions
                    {
                    
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Images = new List<string> { item.Image! },
                                Name = item.ProductName
                            },
                            UnitAmount = (long)(item.Price * 100) // Convert to cents
                        },
                    
                        Quantity = item.Quantity
                    }).ToList(),
                    Mode = "payment",
                    SuccessUrl = domain+ "/success",
                    CancelUrl = domain + "/Auth/login",
                };
                var service = new SessionService();
                Session session = service.Create(options);
                return Ok(new { sessionId = session.Id });

            }
            catch (StripeException e)
            {
                return StatusCode(500, new { error = e.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    
    }
}