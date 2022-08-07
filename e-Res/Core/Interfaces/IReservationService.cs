using Common.Dto.Guests;
using Common.Dto.Reservations;
using Common.Dto.Role;
using Common.Dto.Rooms;
using Common.Dtos.Reservations;
using Common.Dtos.Reviews;
using Core.SearchObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IReservationService
    {
        Task<Message> CreateReservationAsMessageAsync(ReservationCreateDto reservationCreateDto, CancellationToken cancellationToken);
        Task<Message> UpdateReservationAsMessageAsync(Guid reservationId, ReservationUpdateDto reservationUpdateDto, CancellationToken cancellationToken);
        Task<Message> DeleteReservationAsMessageAsync(Guid reservationId, CancellationToken cancellationToken);
        Task<Message> GetReservationAsMessageAsync(Guid Id, CancellationToken cancellationToken);
        Task<Message> GetReservationServicesAsMessageAsync(Guid ReservationId, CancellationToken cancellationToken);
        Task<Message> GetReservationsByCompanyIdAsMessageAsync( CancellationToken cancellationToken);
        Task<Message> GetTopXReservationsByCompanyIdAsMessageAsync( int numberOfReservations, CancellationToken cancellationToken);
        Task<Message> MarkReservationAsFinishedAsMessageAsync(FinishReservationDto finishReservationDto, CancellationToken cancellationToken);
        Task<Message> GetRevenueOfReservationsByLastMonth(CancellationToken cancellationToken);
        Task<Message> GetRevenueOfReservationsByCurrentMonth(CancellationToken cancellationToken);
        Task<Message> GetNumberOfReservationsByCurrentMonth(CancellationToken cancellationToken);
        Task<Message> AddReviewToCompanyAsMessageAsync(ReviewCreateDto reviewCreateDto,CancellationToken cancellationToken);

    }
}
