using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dto
{
    public class BillsGetDto
    {
        public string? FLName { get; set; }
        public bool IsPaid { get; set; }
        public int NumberOfNights { get; set; }
        public double Price { get; set; }
        public double PriceOfServices { get; set; }
        public double TotalAmount { get; set; }
    }
}