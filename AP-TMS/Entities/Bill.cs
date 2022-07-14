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
    public class Bill : BaseEntity
    {
        public bool IsPrinted { get; set; }

        [ForeignKey(nameof(Company))]
        public Guid CompanyId { get; set; }
        public Company? Company { get; set; }

        [ForeignKey(nameof(Reservation))]
        public Guid ReservationId { get; set; }
        public Reservation? Reservation { get; set; }

    }
}
