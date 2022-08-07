using Common.Dto;
using Common.Dto.City;
using Common.Dto.Country;
using Core.Interfaces;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Stripe;
using Stripe.Checkout;

namespace e_Res.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        public IPaymentService paymentService { get; set; }

        public PaymentController(IPaymentService paymentService)
        {
            this.paymentService = paymentService;
        }


        //[HttpPost("add-city"), Authorize(Roles = "Desktop" + "," + "Mobile"+","+"Admin")]
        //public async Task<IActionResult> AddCity(CityCreateDto cityCreateDto, CancellationToken cancellationToken)
        //{
        //    var message = await _cityService.CreateCityAsMessageAsync(cityCreateDto, cancellationToken);
        //    if (message.IsValid == false)
        //        return BadRequest(message);
        //    return Ok(message);
        //}
        public class Item
        {
            [JsonProperty("id")]
            public string Id { get; set; }
        }

        public class PaymentIntentCreateRequest
        {
            [JsonProperty("items")]
            public Item[] Items { get; set; }
        }
        [HttpPost]
        public ActionResult Create(PaymentIntentCreateRequest request)
        {

            StripeConfiguration.ApiKey = "sk_test_51LS4cRFZdZe5rLR67vCn4AqcNBOp7933naZeUCdKREJuHzobqqdK7SfP719aZNPbcEguUyamBHE3Esu0YNv8tCCS007lySqC2s";


            var options = new CustomerCreateOptions
            {
                Description = "My First Test Customer (created for API docs at https://www.stripe.com/docs/api)",
            };
            var service = new CustomerService();
            var n = service.Create(options);

            var options1 = new PaymentMethodCreateOptions
            {
                Type = "card",
                Card = new PaymentMethodCardOptions
                {
                    Number = "4242424242424242",
                    ExpMonth = 8,
                    ExpYear = 2023,
                    Cvc = "314",
                    
                },
            };
            var service1 = new PaymentMethodService();
            var p = service1.Create(options1);

            var paymentIntentService = new PaymentIntentService();
            var paymentIntent = paymentIntentService.Create(new PaymentIntentCreateOptions
            {
                PaymentMethod = p.Id,
                Customer = n.Id,
                Amount = CalculateOrderAmount(request.Items),
                Currency = "eur",
                AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                {
                    Enabled = true,
                },
                Metadata  = new Dictionary<string, string>
    {
        { "ProductID", "price_1LSR2PFZdZe5rLR6Z4H0mIBK" },
    }

            });
            //        StripeConfiguration.ApiKey = "sk_test_51LS4cRFZdZe5rLR67vCn4AqcNBOp7933naZeUCdKREJuHzobqqdK7SfP719aZNPbcEguUyamBHE3Esu0YNv8tCCS007lySqC2s";

            //        var options = new TokenCreateOptions
            //        {
            //            Card = new TokenCardOptions
            //            {
            //                Number = "4242424242424242",
            //                ExpMonth = "8",
            //                ExpYear = "2023",
            //                Cvc = "314",
            //            },
            //        };
            //        var service = new TokenService();
            //        service.Create(options);
            //        var customerOptions = new CustomerCreateOptions
            //        {
            //            Email = "customer@example.com"    , Source= options.Customer
            //        };
            //        var customerService = new CustomerService();
            //        Customer customer = customerService.Create(customerOptions);

            //        var chargeOptions = new ChargeCreateOptions
            //        {
            //            Customer = customer.Id,
            //            Description = "Custom t-shirt",
            //            Amount = 240,
            //            Currency = "usd",
            //            Metadata = new Dictionary<string, string>
            //{
            //    { "OrderId", "6735" },
            //}
            //        };

            return Ok();
        }

        private ActionResult Json(object p)
        {
            throw new NotImplementedException();
        }

        private int CalculateOrderAmount(Item[] items)
        {
            // Replace this constant with a calculation of the order's amount
            // Calculate the order total on the server to prevent
            // people from directly manipulating the amount on the client
            return 2500;
        }
        //[HttpGet("get-cities-by-country-id/{Id}"), Authorize()]
        //public async Task<IActionResult> GetCities(Guid Id, CancellationToken cancellationToken)
        //{
        //    var message = await _cityService.GetCitesByCountryIdAsMessageAsync(Id, cancellationToken);
        //    if (message.IsValid == false)
        //        return BadRequest(message);
        //    return Ok(message);
        //}
    }
}
