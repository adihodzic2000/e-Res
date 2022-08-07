using AutoMapper;
using Common.Dto;
using Common.Dto.Company;
using Common.Dto.Country;
using Common.Dto.Images;
using Common.Dtos.Payment;
using Common.Helper;
using Core.Interfaces;
using Database;
using Entities;
using Entities.Enums;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Stripe;
using Stripe.Checkout;

namespace Core.Services
{
    public class PaymentService : IPaymentService
    {
        public readonly ERESContext _dbContext;
        public IMapper Mapper { get; set; }
        private IAuthContext authContext { get; set; }


        public PaymentService(ERESContext dbContext, IMapper mapper, IAuthContext authContext)
        {
            _dbContext = dbContext;
            Mapper = mapper;
            this.authContext = authContext;
        }

        public void createMainThing()
        {
            StripeConfiguration.ApiKey = "sk_test_51LS4cRFZdZe5rLR67vCn4AqcNBOp7933naZeUCdKREJuHzobqqdK7SfP719aZNPbcEguUyamBHE3Esu0YNv8tCCS007lySqC2s";

            var options = new ProductCreateOptions { Name = "Prenociste" };
            var service = new ProductService();
            service.Create(options);
        }
        public void createPrice()
        {
            StripeConfiguration.ApiKey = "sk_test_51LS4cRFZdZe5rLR67vCn4AqcNBOp7933naZeUCdKREJuHzobqqdK7SfP719aZNPbcEguUyamBHE3Esu0YNv8tCCS007lySqC2s";

            var options = new PriceCreateOptions
            {
                Product = "{{PRODUCT_ID}}",
                UnitAmount = 2000,
                Currency = "usd",
            };
            var service = new PriceService();
            service.Create(options);
        }



        public async Task<Message> InitialStart(string name, string email, string systemId, CancellationToken cancellation)
        {
            try
            {

                createMainThing();
                createPrice();
                return new Message { Info = "Passed", IsValid = true, Status = ExceptionCode.Success };
            }
            catch (Exception ex)
            {
                return new Message { Info = "Passed", IsValid = true, Status = ExceptionCode.Success };
            }
        }
    }
}

