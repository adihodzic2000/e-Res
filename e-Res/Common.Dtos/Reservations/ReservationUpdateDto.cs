using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto.Reservations
{
    public class ReservationUpdateDto
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        //Moguće random biranje sobe ili da korisnik odabere sa mobilne aplikacije
        public Guid RoomId { get; set; }
        
        //U slučaju da desktop korisnik može dodati rezervaciju
        public Guid GuestId { get; set; }

    }
}
