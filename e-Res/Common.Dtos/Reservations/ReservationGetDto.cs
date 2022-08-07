using Common.Dto.Guests;
using Common.Dto.Rooms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto.Reservations
{
    public class ReservationGetDto
    {
        public Guid Id { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public double TotalAmount { get; set; }
        public GuestGetDto Guest { get; set; }
        public RoomGetDto Room { get; set; }
        public bool IsFinished { get; set; }

    }
}
