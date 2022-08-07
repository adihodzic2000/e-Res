using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Bill : BaseEntity
    {
        public bool IsPaid { get; set; }
        public double TotalAmountOfNights { get; set; }
        public double TotalAmountOfServices { get; set; }
        public double PriceOfNight { get; set; }
        public double TotalAmount { get; set; }
        public DateTime? PaidDate { get; set; }
        [ForeignKey(nameof(Company))]
        public Guid CompanyId { get; set; }
        public Company? Company { get; set; }

        [ForeignKey(nameof(Reservation))]
        public Guid ReservationId { get; set; }
        public Reservation? Reservation { get; set; }

    }
}
