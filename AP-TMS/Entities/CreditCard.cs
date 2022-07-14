using Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class CreditCard : BaseEntity
    {
        public string? CreditNumber { get; set; }//encode 64

        [ForeignKey(nameof(Reservation))]
        public Guid ReservationId { get; set; }
        public Reservation? Reservation { get; set; }

    }
}
