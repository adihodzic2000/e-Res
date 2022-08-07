using Common.Dto.Company;
using Common.Dto.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto.Bills
{
    public class BillsCreateDto
    {
        public double TotalAmountOfNights { get; set; }
        public double TotalAmountOfServices { get; set; }
        public double PriceOfNight { get; set; }
        public double TotalAmount { get; set; }
        public Guid CompanyId { get; set; }
        public Guid ReservationId { get; set; }
    }
}