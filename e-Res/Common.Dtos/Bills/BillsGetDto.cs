using Common.Dto.Company;
using Common.Dto.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dto.Bills
{
    public class BillsGetDto
    {
        public Guid Id { get; set; }
        public bool IsPaid { get; set; }
        public double TotalAmountOfNights { get; set; }
        public double TotalAmountOfServices { get; set; }
        public double PriceOfNight { get; set; }
        public double TotalAmount { get; set; }
        public CompanyGetDto Company { get; set; }
        public ReservationGetDto Reservation { get; set; }
    }
}