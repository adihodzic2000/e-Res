using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.Dtos
{
    public class AddingServiceDto
    {
        public int rb { get; set; }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Quantity { get; set; }
        public double TotalAmount { get; set; }
    }
}
