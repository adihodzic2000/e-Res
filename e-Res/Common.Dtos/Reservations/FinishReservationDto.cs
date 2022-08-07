using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dtos.Reservations
{
    public class FinishReservationDto
    {
        public Guid Id { get; set; }
        public bool IsFinished { get; set; }
    }
}
