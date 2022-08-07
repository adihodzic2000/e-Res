using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Dtos.Payment
{
    public class MakePaymentDto
    {
        public string CardNumber { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public string CVC { get; set; }
        public int Value { get; set; }
    }
}
