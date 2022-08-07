using Common.Dto.Guests;
using Common.Dto.Reservations;
using Common.Dto.Rooms;
using Common.Dtos.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto.ReservationServices
{
    public class ReservationServicesGetDto
    {
        public Guid Id { get; set; }
        public ServicesGetDto? Service { get; set; }
        public int Quantity { get; set; }

        public ReservationGetDto? Reservation { get; set; }

    }
}
