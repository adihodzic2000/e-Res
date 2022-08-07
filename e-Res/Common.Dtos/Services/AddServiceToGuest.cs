using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dtos.Services
{
    public class AddServiceToGuest
    {
        public Guid ReservationId { get; set; }
        public Guid ServiceId { get; set; }
        public int Quantity { get; set; }
    }
}
