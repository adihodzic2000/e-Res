using Common.Dto;
using Common.Dto.City;
using Common.Dto.Country;
using Common.Dtos.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IPaymentService
    {
       Task<Message> InitialStart(string name, string email, string systemId, CancellationToken cancellation);
    }
}
