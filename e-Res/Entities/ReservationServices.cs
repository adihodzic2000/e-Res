using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ReservationServices : BaseEntity
    {
        [ForeignKey(nameof(Service))]
        public Guid ServiceId { get; set; }
        public Service? Service { get; set; }

        [ForeignKey(nameof(Reservation))]
        public Guid ReservationId { get; set; }
        public Reservation? Reservation { get; set; }

        public int Quantity { get; set; }

    }
}
