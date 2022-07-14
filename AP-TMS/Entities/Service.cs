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
    public class Service : BaseEntity
    {
        public string? Title { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
