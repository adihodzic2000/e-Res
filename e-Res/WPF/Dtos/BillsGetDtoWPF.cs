using Common.Dto.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Dtos
{
    public class BillsGetDtoWPF
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string IsPaid { get; set; }
        public double NumberOfNights { get; set; }
        public double Price { get; set; }
        public double PriceOfServices { get; set; }
        public double TotalAmount { get; set; }
        public ReservationGetDto Reservation { get; set; }
    }
}