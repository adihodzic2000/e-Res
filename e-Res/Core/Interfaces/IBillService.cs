using Common.Dto.Bills;
using Common.Dto.Guests;
using Common.Dto.Role;
using Core.SearchObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IBillService
    {
        Task<Message> CreateBillAsMessageAsync(BillsCreateDto billsCreateDto, CancellationToken cancellationToken);
        Task<Message> UpdateBillAsMessageAsync(Guid billId, BillsUpdateDto billsUpdateDto, CancellationToken cancellationToken);
        Task<Message> DeleteBillAsMessageAsync(Guid billId, CancellationToken cancellationToken);
        Task<Message> GetBillAsMessageAsync(Guid billId, CancellationToken cancellationToken);
        Task<Message> GetBillsByCompanyIdAsMessageAsync(SearchObject searchObject, CancellationToken cancellationToken);
        Task<Message> GetBillsByGuestIdAsMessageAsync(Guid billId, CancellationToken cancellationToken);
        Task<Message> PayBillAsMessageAsync(BillsPayDto billsPayDto, CancellationToken cancellationToken);

    }
}
