using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Reservation : BaseEntity
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public double TotalAmount{ get; set; }

        [ForeignKey(nameof(Guest))]//U slučaju da desktop korisnik može dodati rezervaciju
        public Guid GuestId { get; set; }
        public Guest? Guest { get; set; }

        [ForeignKey(nameof(Room))]
        public Guid RoomId { get; set; }
        public Room? Room { get; set; }

        public bool IsFinished { get; set; }
    }
}
